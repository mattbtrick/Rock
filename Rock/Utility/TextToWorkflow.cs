﻿// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Rock;
using Rock.Model;
using Rock.Cache;

namespace Rock.Utility
{
    /// <summary>
    /// Utility class used by the TextToWorkflow webhook
    /// </summary>
    public static class TextToWorkflow
    {
        /// <summary>
        /// Handles a recieved message
        /// </summary>
        /// <param name="toPhone">To phone.</param>
        /// <param name="fromPhone">From phone.</param>
        /// <param name="message">The message.</param>
        /// <param name="response">The response.</param>
        public static void MessageRecieved( string toPhone, string fromPhone, string message, out string response )
        {
            response = string.Empty;
            bool foundWorkflow = false;

            // get TextToWorkflow defined types for this number
            var definedType = CacheDefinedType.Get( Rock.SystemGuid.DefinedType.TEXT_TO_WORKFLOW.AsGuid() );
            if ( definedType != null && definedType.DefinedValues != null && definedType.DefinedValues.Any() )
            {
                var smsWorkflows = definedType.DefinedValues.Where( v => v.Value.AsNumeric() == toPhone.AsNumeric() ).OrderBy( v => v.Order ).ToList();

                // iterate through workflows looking for a keyword match
                foreach ( CacheDefinedValue dvWorkflow in smsWorkflows )
                {
                    string keywordExpression = dvWorkflow.GetAttributeValue( "KeywordExpression" );
                    //string workflowAttributes = dvWorkflow.GetAttributeValue( "WorkflowAttributes" );
                    string nameTemplate = dvWorkflow.GetAttributeValue( "WorkflowNameTemplate" );
                    List<KeyValuePair<string, object>> workflowAttributesSettings = new List<KeyValuePair<string, object>>();

                    var workflowAttributes = dvWorkflow.Attributes["WorkflowAttributes"];
                    if ( workflowAttributes != null )
                    {
                        var field = workflowAttributes.FieldType.Field;
                        if ( field is Rock.Field.Types.KeyValueListFieldType )
                        {
                            var keyValueField = ( Rock.Field.Types.KeyValueListFieldType ) field;

                            workflowAttributesSettings = keyValueField.GetValuesFromString( null, dvWorkflow.GetAttributeValue( "WorkflowAttributes" ), workflowAttributes.QualifierValues, false );
                        }
                    }


                    // if not keyword expression add wildcard expression
                    if ( string.IsNullOrWhiteSpace( keywordExpression ) )
                    {
                        keywordExpression = ".*";
                    }

                    // if the keyword is just a * then replace it
                    if ( keywordExpression == "*" )
                    {
                        keywordExpression = ".*";
                    }

                    // Prefix keyword with start-of-string assertion (input needs to start with selected expression)
                    if ( !keywordExpression.StartsWith( "^") )
                    {
                        keywordExpression = $"^{keywordExpression}";
                    }

                    if ( !string.IsNullOrWhiteSpace( keywordExpression ) )
                    {
                        Match match = Regex.Match( message, keywordExpression, RegexOptions.IgnoreCase );
                        if ( match.Success )
                        {
                            foundWorkflow = true;

                            var workflowTypeGuid = dvWorkflow.GetAttributeValue( "WorkflowType" ).AsGuidOrNull();
                            if ( workflowTypeGuid.HasValue )
                            {
                                // launch workflow
                                using ( var rockContext = new Data.RockContext() )
                                {
                                    var personService = new PersonService( rockContext );
                                    var phoneNumberService = new PhoneNumberService(rockContext);
                                    var groupMemberService = new GroupMemberService( rockContext );

                                    var workflowType = CacheWorkflowType.Get( workflowTypeGuid.Value );
                                    if ( workflowType != null )
                                    {
                                        Person fromPerson = null;

                                        // Activate a new workflow
                                        var workflow = Rock.Model.Workflow.Activate( workflowType, "Request from " + ( fromPhone ?? "??" ), rockContext );

                                        // give preference to people with the phone in the mobile phone type
                                        // first look for a person with the phone number as a mobile phone order by family role then age
                                        var mobilePhoneType = CacheDefinedValue.Get( Rock.SystemGuid.DefinedValue.PERSON_PHONE_TYPE_MOBILE );
                                        var familyGroupType = CacheGroupType.Get( Rock.SystemGuid.GroupType.GROUPTYPE_FAMILY );

                                        string phoneNumber = fromPhone.Replace("+", "");

                                        // Get people with matching mobile number query the database only by number to increase chance of using index.
                                        var peopleWithMobileNumber = phoneNumberService
                                            .Queryable().AsNoTracking()
                                            .Where(n => ( n.CountryCode ?? "" ) + (n.Number ?? "" ) == phoneNumber )
                                            .Select(n => new
                                            {
                                                n.PersonId,
                                                n.NumberTypeValueId
                                            })
                                            .ToList()   // Query database only on number, then filter by type in memory
                                            .Where(v => v.NumberTypeValueId == mobilePhoneType.Id)
                                            .Select(v => v.PersonId)
                                            .ToList();

                                        if (peopleWithMobileNumber.Any())
                                        {
                                            fromPerson = groupMemberService.Queryable()
                                                .Where(m =>
                                                    m.Group.GroupTypeId == familyGroupType.Id &&
                                                    peopleWithMobileNumber.Contains(m.PersonId))
                                                .OrderBy(m => m.GroupRole.Order)
                                                .ThenBy(m => m.Person.BirthDate ?? DateTime.MinValue)
                                                .Select(m => m.Person)
                                                .FirstOrDefault();
                                        }

                                        // if no match then look for the phone in any phone type ordered by family role then age
                                        if ( fromPerson == null )
                                        {
                                            var peopleWithAnyNumber = phoneNumberService
                                                .Queryable().AsNoTracking()
                                                .Where(n => (n.CountryCode ?? "") + (n.Number ?? "") == phoneNumber)
                                                .Select(n => n.PersonId)
                                                .ToList();

                                            if ( peopleWithAnyNumber.Any())
                                            {
                                                fromPerson = groupMemberService.Queryable()
                                                    .Where(m =>
                                                        m.Group.GroupTypeId == familyGroupType.Id &&
                                                        peopleWithAnyNumber.Contains(m.PersonId))
                                                    .OrderBy(m => m.GroupRole.Order)
                                                    .ThenBy(m => m.Person.BirthDate ?? DateTime.MinValue)
                                                    .Select(m => m.Person).FirstOrDefault();
                                            }
                                        }

                                        // Set initiator
                                        if ( fromPerson != null )
                                        {
                                            workflow.InitiatorPersonAliasId = fromPerson.PrimaryAliasId;
                                        }

                                        // create merge object
                                        var formattedPhoneNumber = PhoneNumber.CleanNumber( PhoneNumber.FormattedNumber( PhoneNumber.DefaultCountryCode(), fromPhone ) );

                                        List<string> matchGroups = new List<string>();
                                        foreach ( var matchItem in match.Groups )
                                        {
                                            matchGroups.Add( matchItem.ToString() );
                                        }

                                        Dictionary<string, object> mergeValues = new Dictionary<string, object>();
                                        mergeValues.Add( "FromPhone", formattedPhoneNumber );
                                        mergeValues.Add( "ToPhone", toPhone );
                                        mergeValues.Add( "MessageBody", message );
                                        mergeValues.Add( "MatchedGroups", matchGroups );
                                        mergeValues.Add( "ReceivedTime", RockDateTime.Now.ToString( "HH:mm:ss" ) );
                                        mergeValues.Add( "ReceivedDate", RockDateTime.Now.ToShortDateString() );
                                        mergeValues.Add( "ReceivedDateTime", RockDateTime.Now.ToString( "o" ) );
                                        mergeValues.Add( "FromPerson", fromPerson );

                                        // add phone number attribute
                                        workflow.SetAttributeValue( "FromPhone", fromPhone );

                                        // set workflow attributes
                                        foreach( var attribute in workflowAttributesSettings )
                                        {
                                            workflow.SetAttributeValue( attribute.Key, attribute.Value.ToString().ResolveMergeFields( mergeValues ) );
                                        }
                                        
                                        // set workflow name
                                        string name = nameTemplate.ResolveMergeFields( mergeValues );
                                        if ( name.IsNotNullOrWhitespace() )
                                        {
                                            workflow.Name = name;
                                        }

                                        // process the workflow
                                        List<string> workflowErrors;
                                        new Rock.Model.WorkflowService( rockContext ).Process( workflow, out workflowErrors );

                                        // check to see if there is a response to return
                                        string responseAttribute = workflow.GetAttributeValue( "SMSResponse" );
                                        if ( responseAttribute != null && !string.IsNullOrWhiteSpace( responseAttribute ) )
                                        {
                                            response = responseAttribute;
                                        }

                                    }
                                    else
                                    {
                                        response = "This keyword is no longer valid.";
                                    }
                                }
                            }
                            else
                            {
                                response = "No response could be provided for this keyword.";
                            }


                            // once we find one match stop processing
                            break;
                        }
                    }
                }

                if ( !foundWorkflow )
                {
                    response = "The keyword you provided was not valid. ";
                }

            }

        }

    }
}
