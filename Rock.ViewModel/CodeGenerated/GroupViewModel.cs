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
//

using System;
using System.Linq;

namespace Rock.ViewModel
{
    /// <summary>
    /// Group View Model
    /// </summary>
    public partial class GroupViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the AllowGuests.
        /// </summary>
        /// <value>
        /// The AllowGuests.
        /// </value>
        public bool? AllowGuests { get; set; }

        /// <summary>
        /// Gets or sets the ArchivedByPersonAliasId.
        /// </summary>
        /// <value>
        /// The ArchivedByPersonAliasId.
        /// </value>
        public int? ArchivedByPersonAliasId { get; set; }

        /// <summary>
        /// Gets or sets the ArchivedDateTime.
        /// </summary>
        /// <value>
        /// The ArchivedDateTime.
        /// </value>
        public DateTime? ArchivedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the AttendanceRecordRequiredForCheckIn.
        /// </summary>
        /// <value>
        /// The AttendanceRecordRequiredForCheckIn.
        /// </value>
        public int AttendanceRecordRequiredForCheckIn { get; set; }

        /// <summary>
        /// Gets or sets the CampusId.
        /// </summary>
        /// <value>
        /// The CampusId.
        /// </value>
        public int? CampusId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the DisableScheduleToolboxAccess.
        /// </summary>
        /// <value>
        /// The DisableScheduleToolboxAccess.
        /// </value>
        public bool DisableScheduleToolboxAccess { get; set; }

        /// <summary>
        /// Gets or sets the DisableScheduling.
        /// </summary>
        /// <value>
        /// The DisableScheduling.
        /// </value>
        public bool DisableScheduling { get; set; }

        /// <summary>
        /// Gets or sets the GroupCapacity.
        /// </summary>
        /// <value>
        /// The GroupCapacity.
        /// </value>
        public int? GroupCapacity { get; set; }

        /// <summary>
        /// Gets or sets the GroupTypeId.
        /// </summary>
        /// <value>
        /// The GroupTypeId.
        /// </value>
        public int GroupTypeId { get; set; }

        /// <summary>
        /// Gets or sets the InactiveDateTime.
        /// </summary>
        /// <value>
        /// The InactiveDateTime.
        /// </value>
        public DateTime? InactiveDateTime { get; set; }

        /// <summary>
        /// Gets or sets the InactiveReasonNote.
        /// </summary>
        /// <value>
        /// The InactiveReasonNote.
        /// </value>
        public string InactiveReasonNote { get; set; }

        /// <summary>
        /// Gets or sets the InactiveReasonValueId.
        /// </summary>
        /// <value>
        /// The InactiveReasonValueId.
        /// </value>
        public int? InactiveReasonValueId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsArchived.
        /// </summary>
        /// <value>
        /// The IsArchived.
        /// </value>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Gets or sets the IsPublic.
        /// </summary>
        /// <value>
        /// The IsPublic.
        /// </value>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the IsSecurityRole.
        /// </summary>
        /// <value>
        /// The IsSecurityRole.
        /// </value>
        public bool IsSecurityRole { get; set; }

        /// <summary>
        /// Gets or sets the IsSystem.
        /// </summary>
        /// <value>
        /// The IsSystem.
        /// </value>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        /// <value>
        /// The Order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the ParentGroupId.
        /// </summary>
        /// <value>
        /// The ParentGroupId.
        /// </value>
        public int? ParentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the RequiredSignatureDocumentTemplateId.
        /// </summary>
        /// <value>
        /// The RequiredSignatureDocumentTemplateId.
        /// </value>
        public int? RequiredSignatureDocumentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the RSVPReminderOffsetDays.
        /// </summary>
        /// <value>
        /// The RSVPReminderOffsetDays.
        /// </value>
        public int? RSVPReminderOffsetDays { get; set; }

        /// <summary>
        /// Gets or sets the RSVPReminderSystemCommunicationId.
        /// </summary>
        /// <value>
        /// The RSVPReminderSystemCommunicationId.
        /// </value>
        public int? RSVPReminderSystemCommunicationId { get; set; }

        /// <summary>
        /// Gets or sets the ScheduleCancellationPersonAliasId.
        /// </summary>
        /// <value>
        /// The ScheduleCancellationPersonAliasId.
        /// </value>
        public int? ScheduleCancellationPersonAliasId { get; set; }

        /// <summary>
        /// Gets or sets the ScheduleId.
        /// </summary>
        /// <value>
        /// The ScheduleId.
        /// </value>
        public int? ScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the SchedulingMustMeetRequirements.
        /// </summary>
        /// <value>
        /// The SchedulingMustMeetRequirements.
        /// </value>
        public bool SchedulingMustMeetRequirements { get; set; }

        /// <summary>
        /// Gets or sets the StatusValueId.
        /// </summary>
        /// <value>
        /// The StatusValueId.
        /// </value>
        public int? StatusValueId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateTime.
        /// </summary>
        /// <value>
        /// The CreatedDateTime.
        /// </value>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDateTime.
        /// </summary>
        /// <value>
        /// The ModifiedDateTime.
        /// </value>
        public DateTime? ModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByPersonAliasId.
        /// </summary>
        /// <value>
        /// The CreatedByPersonAliasId.
        /// </value>
        public int? CreatedByPersonAliasId { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedByPersonAliasId.
        /// </summary>
        /// <value>
        /// The ModifiedByPersonAliasId.
        /// </value>
        public int? ModifiedByPersonAliasId { get; set; }

    }
}
