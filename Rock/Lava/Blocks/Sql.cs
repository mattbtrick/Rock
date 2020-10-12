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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Rock.Data;

namespace Rock.Lava.Blocks
{
    /// <summary>
    /// Sql stores the result of provided SQL query into a variable.
    ///
    /// {% sql results %}
    /// SELECT [FirstName], [LastName] FROM [Person]
    /// {% endsql %}
    /// </summary>
    public class Sql : RockLavaBlockBase
    {
        private static readonly Regex Syntax = new Regex( @"(\w+)" );

        string _markup = string.Empty;

        /// <summary>
        /// Initializes the specified tag name.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="markup">The markup.</param>
        /// <param name="tokens">The tokens.</param>
        /// <exception cref="System.Exception">Could not find the variable to place results in.</exception>
        public override void OnInitialize( string tagName, string markup, List<string> tokens )
        {
            _markup = markup;

            base.OnInitialize( tagName, markup, tokens );
        }

        /// <summary>
        /// Renders the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        public override void OnRender( ILavaContext context, TextWriter result )
        {
            // first ensure that sql commands are allowed in the context
            if ( !this.IsAuthorized( context ) )
            {
                result.Write( string.Format( RockLavaBlockBase.NotAuthorizedMessage, this.BlockName ) );
                base.OnRender( context, result );
                return;
            }

            using ( TextWriter sql = new StringWriter() )
            {
                base.OnRender( context, sql );

                var parms = ParseMarkup( _markup, context );

                var sqlTimeout = ( int? ) null;
                if ( parms.ContainsKey( "timeout" ) )
                {
                    sqlTimeout = parms["timeout"].AsIntegerOrNull();
                }
                
                switch ( parms["statement"] )
                {
                    case "select":
                        var results = DbService.GetDataSet( sql.ToString(), CommandType.Text, parms.ToDictionary( i => i.Key, i => ( object ) i.Value ), sqlTimeout );

                        context.SetMergeFieldValue( parms["return"], results.Tables[0].ToDynamic(), "root"  );
                        break;
                    case "command":
                        var sqlParameters = new List<System.Data.SqlClient.SqlParameter>();

                        foreach ( var p in parms )
                        {
                            sqlParameters.Add( new System.Data.SqlClient.SqlParameter( p.Key, p.Value ) );
                        }

                        using ( var rockContext = new RockContext() )
                        {
                            if ( sqlTimeout != null )
                            {
                                rockContext.Database.CommandTimeout = sqlTimeout;
                            }
                            int numOfRowsAffected = rockContext.Database.ExecuteSqlCommand( sql.ToString(), sqlParameters.ToArray() );

                            context.SetMergeFieldValue( parms["return"], numOfRowsAffected, "root" );
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Parses the markup.
        /// </summary>
        /// <param name="markup">The markup.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private Dictionary<string, string> ParseMarkup( string markup, ILavaContext context )
        {
            // first run lava across the inputted markup
            var internalMergeFields = context.GetMergeFieldsForLocalScope();

            var parms = new Dictionary<string, string>();
            parms.Add( "return", "results" );
            parms.Add( "statement", "select" );
            
            var markupItems = Regex.Matches( markup, @"(\S*?:'[^']+')" )
                .Cast<Match>()
                .Select( m => m.Value )
                .ToList();

            foreach ( var item in markupItems )
            {
                var itemParts = item.ToString().Split( new char[] { ':' }, 2 );
                if ( itemParts.Length > 1 )
                {
                    var value = itemParts[1];

                    if ( value.HasMergeFields() )
                    {
                        value = value.ResolveMergeFields( internalMergeFields );
                    }

                    parms.AddOrReplace( itemParts[0].Trim().ToLower(), value.Substring( 1, value.Length - 2 ).Trim() );
                }
            }
            return parms;
        }

    }
}