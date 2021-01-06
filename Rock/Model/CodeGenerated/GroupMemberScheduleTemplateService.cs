//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// <copyright>
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

using System;
using System.Linq;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// GroupMemberScheduleTemplate Service class
    /// </summary>
    public partial class GroupMemberScheduleTemplateService : Service<GroupMemberScheduleTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMemberScheduleTemplateService"/> class
        /// </summary>
        /// <param name="context">The context.</param>
        public GroupMemberScheduleTemplateService(RockContext context) : base(context)
        {
        }

        /// <summary>
        /// Determines whether this instance can delete the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///   <c>true</c> if this instance can delete the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDelete( GroupMemberScheduleTemplate item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<GroupMember>( Context ).Queryable().Any( a => a.ScheduleTemplateId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", GroupMemberScheduleTemplate.FriendlyTypeName, GroupMember.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class GroupMemberScheduleTemplateExtensionMethods
    {
        /// <summary>
        /// Clones this GroupMemberScheduleTemplate object to a new GroupMemberScheduleTemplate object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static GroupMemberScheduleTemplate Clone( this GroupMemberScheduleTemplate source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as GroupMemberScheduleTemplate;
            }
            else
            {
                var target = new GroupMemberScheduleTemplate();
                target.CopyPropertiesFrom( source );
                return target;
            }
        }

        /// <summary>
        /// Clones this GroupMemberScheduleTemplate object to a new GroupMemberScheduleTemplate object with default values for the properties in the Entity and Model base classes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static GroupMemberScheduleTemplate CloneWithoutIdentity( this GroupMemberScheduleTemplate source )
        {
            var target = new GroupMemberScheduleTemplate();
            target.CopyPropertiesFrom( source );

            target.Id = 0;
            target.Guid = Guid.NewGuid();
            target.ForeignKey = null;
            target.ForeignId = null;
            target.ForeignGuid = null;
            target.CreatedByPersonAliasId = null;
            target.CreatedDateTime = RockDateTime.Now;
            target.ModifiedByPersonAliasId = null;
            target.ModifiedDateTime = RockDateTime.Now;

            return target;
        }

        /// <summary>
        /// Copies the properties from another GroupMemberScheduleTemplate object to this GroupMemberScheduleTemplate object
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void CopyPropertiesFrom( this GroupMemberScheduleTemplate target, GroupMemberScheduleTemplate source )
        {
            target.Id = source.Id;
            target.ForeignGuid = source.ForeignGuid;
            target.ForeignKey = source.ForeignKey;
            target.GroupTypeId = source.GroupTypeId;
            target.Name = source.Name;
            target.ScheduleId = source.ScheduleId;
            target.CreatedDateTime = source.CreatedDateTime;
            target.ModifiedDateTime = source.ModifiedDateTime;
            target.CreatedByPersonAliasId = source.CreatedByPersonAliasId;
            target.ModifiedByPersonAliasId = source.ModifiedByPersonAliasId;
            target.Guid = source.Guid;
            target.ForeignId = source.ForeignId;

        }
    }
}
