﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
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
using System.Linq;

using Rock.Model;
using Rock.Transactions;

namespace Rock.Web.UI
{
    /// <summary>
    /// A Block used on the person detail page
    /// </summary>
    [ContextAware( typeof(Person) )]
    public class PersonBlock : RockBlock
    {
        #region Properties

        /// <summary>
        /// The current person being viewed
        /// </summary>
        public Person Person { get; set; }

        #endregion

        #region Base Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            Person = this.ContextEntity<Person>();

            if ( Person == null )
            {
                Person = new Person();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( !Page.IsPostBack && 
                Context.Items["PersonViewed"] == null &&
                Person != null && 
                CurrentPersonId.HasValue && 
                Person.Id != CurrentPersonId.Value )
            {
                var transaction = new PersonViewTransaction();
                transaction.DateTimeViewed = DateTime.Now;
                transaction.TargetPersonId = Person.Id;
                transaction.ViewerPersonId = CurrentPersonId.Value;
                transaction.Source = RockPage.PageTitle;
                transaction.IPAddress = Request.UserHostAddress;
                RockQueue.TransactionQueue.Enqueue( transaction );

                Context.Items.Add( "PersonViewed", "Handled" );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The groups of a particular type that current person belongs to
        /// </summary>
        /// <param name="groupTypeGuid">The group type GUID.</param>
        /// <returns></returns>
        public IEnumerable<Group> PersonGroups( string groupTypeGuid )
        {
            return PersonGroups( new Guid( groupTypeGuid ) );
        }

        /// <summary>
        /// The groups of a particular type that current person belongs to
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> PersonGroups(Guid groupTypeGuid)
        {
            string itemKey = "RockGroups:" + groupTypeGuid.ToString();

            if ( Context.Items.Contains( itemKey ) )
            {
                return PersonGroups( (int)Context.Items[itemKey] );
            }

            var groupType = Rock.Web.Cache.GroupTypeCache.Read( groupTypeGuid );
            int groupTypeId = groupType != null ? groupType.Id : 0;
            Context.Items.Add( itemKey, groupTypeId );

            return PersonGroups( groupTypeId );
        }

        /// <summary>
        /// The groups of a particular type that current person belongs to
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> PersonGroups( int groupTypeId )
        {
            string itemKey = "RockGroups:" + groupTypeId.ToString();

            var groups = Context.Items[itemKey] as IEnumerable<Group>;
            if ( groups != null )
            {
                return groups;
            }

            if ( Person == null )
            {
                return null;
            }

            var service = new GroupMemberService();
            groups = service.Queryable()
                .Where( m =>
                    m.PersonId == Person.Id &&
                    m.Group.GroupTypeId == groupTypeId )
                .Select( m => m.Group )
                .OrderByDescending( g => g.Name );

            Context.Items.Add( itemKey, groups );

            return groups;
        }

        #endregion
    }

}