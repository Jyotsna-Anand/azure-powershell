// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Sql.TransparentDataEncryption.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.TransparentDataEncryption.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzureRmSqlServerTransparentDataEncryptionProtector cmdlet
    /// </summary>
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlManagedInstanceTransparentDataEncryptionProtector", SupportsShouldProcess = true, DefaultParameterSetName = DefaultParameterSet)]
    [Alias("Get-" + ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlManagedInstanceTDEProtector")]
    [OutputType(typeof(AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel))]
    public class GetAzureRmSqlManagedInstanceTransparentDataEncryptionProtector : AzureSqlRmManagedInstanceTransparentDataEncryptionProtectorBase
    {
        /// <summary>
        /// Get the entities from the service
        /// </summary>
        /// <returns>The list of entities</returns>
        protected override IEnumerable<AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel> GetEntity()
        {
            IList<AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel> resultList = new List<AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel>();

            AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel model = new AzureRmSqlManagedInstanceTransparentDataEncryptionProtectorModel(
                resourceGroupName: this.ResourceGroupName,
                managedInstanceName: this.ManagedInstanceName);

            resultList.Add(ModelAdapter.GetAzureRmSqlManagedInstanceTransparentDataEncryptionProtector(model));

            return resultList;
        }
    }
}