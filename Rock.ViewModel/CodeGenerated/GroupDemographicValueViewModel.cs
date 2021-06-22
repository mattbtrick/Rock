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
    /// GroupDemographicValue View Model
    /// </summary>
    public partial class GroupDemographicValueViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the GroupDemographicTypeId.
        /// </summary>
        /// <value>
        /// The GroupDemographicTypeId.
        /// </value>
        public int GroupDemographicTypeId { get; set; }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        /// <value>
        /// The GroupId.
        /// </value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the LastCalculatedDateTime.
        /// </summary>
        /// <value>
        /// The LastCalculatedDateTime.
        /// </value>
        public DateTime? LastCalculatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the RelatedEntityId.
        /// </summary>
        /// <value>
        /// The RelatedEntityId.
        /// </value>
        public int? RelatedEntityId { get; set; }

        /// <summary>
        /// Gets or sets the RelatedEntityTypeId.
        /// </summary>
        /// <value>
        /// The RelatedEntityTypeId.
        /// </value>
        public int? RelatedEntityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        /// <value>
        /// The Value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the ValueAsBoolean.
        /// </summary>
        /// <value>
        /// The ValueAsBoolean.
        /// </value>
        public bool? ValueAsBoolean { get; set; }

        /// <summary>
        /// Gets or sets the ValueAsGuid.
        /// </summary>
        /// <value>
        /// The ValueAsGuid.
        /// </value>
        public Guid? ValueAsGuid { get; set; }

        /// <summary>
        /// Gets or sets the ValueAsNumeric.
        /// </summary>
        /// <value>
        /// The ValueAsNumeric.
        /// </value>
        public decimal? ValueAsNumeric { get; set; }

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
