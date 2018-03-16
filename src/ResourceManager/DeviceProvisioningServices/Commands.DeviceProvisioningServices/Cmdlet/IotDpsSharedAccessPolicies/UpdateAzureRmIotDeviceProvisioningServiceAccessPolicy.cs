﻿// ----------------------------------------------------------------------------------
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

namespace Microsoft.Azure.Commands.Management.DeviceProvisioningServices
{
    using System;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.DeviceProvisioningServices.Properties;
    using Microsoft.Azure.Commands.Management.DeviceProvisioningServices.Models;
    using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
    using Microsoft.Azure.Management.DeviceProvisioningServices;
    using Microsoft.Azure.Management.DeviceProvisioningServices.Models;
    using System.Collections;
    using System.Collections.Generic;

    [Cmdlet(VerbsData.Update, "AzureRmIoTDeviceProvisioningServiceAccessPolicy", DefaultParameterSetName = ResourceParameterSet, SupportsShouldProcess = true)]
    [Alias("Update-AzureRmIoTDpsAccessPolicy")]
    [OutputType(typeof(PSSharedAccessSignatureAuthorizationRuleAccessRightsDescription))]
    public class UpdateAzureRmIoTDeviceProvisioningServiceAccessPolicy : IotDpsBaseCmdlet
    {
        private const string ResourceIdParameterSet = "ResourceIdSet";
        private const string ResourceParameterSet = "ResourceSet";
        private const string InputObjectParameterSet = "InputObjectSet";

        [Parameter(
            Position = 0,
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            HelpMessage = "IoT Device Provisioning Service Object")]
        [ValidateNotNullOrEmpty]
        public PSProvisioningServiceDescription InputObject { get; set; }

        [Parameter(
            Position = 0,
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service Resource Id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Position = 0,
            Mandatory = true,
            ParameterSetName = ResourceParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Name of the Resource Group")]
        [ResourceGroupCompleter]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = ResourceParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Name of the IoT Device Provisioning Service")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy key name")]
        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy key name")]
        [Parameter(
            Position = 2,
            Mandatory = true,
            ParameterSetName = ResourceParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy key name")]
        [ValidateNotNullOrEmpty]
        public string KeyName { get; set; }

        [Parameter(
            Position = 2,
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy permissions")]
        [Parameter(
            Position = 2,
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy permissions")]
        [Parameter(
            Position = 3,
            Mandatory = true,
            ParameterSetName = ResourceParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "IoT Device Provisioning Service access policy permissions")]
        [ValidateNotNullOrEmpty]
        public string Permissions { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, Resources.UpdateAccessPolicy))
            {
                switch (ParameterSetName)
                {
                    case InputObjectParameterSet:
                        this.ResourceGroupName = this.InputObject.ResourceGroupName;
                        this.Name = this.InputObject.Name;
                        this.UpdateIotDpsAccessPolicy();
                        break;

                    case ResourceIdParameterSet:
                        this.ResourceGroupName = IotDpsUtils.GetResourceGroupName(this.ResourceId);
                        this.Name = IotDpsUtils.GetIotDpsName(this.ResourceId);
                        this.UpdateIotDpsAccessPolicy();
                        break;

                    case ResourceParameterSet:
                        this.UpdateIotDpsAccessPolicy();
                        break;

                    default:
                        throw new ArgumentException("BadParameterSetName");
                }
            }
        }

        private void UpdateIotDpsAccessPolicy()
        {
            string[] permissions = this.Permissions.Split(',');
            ArrayList accessRights = new ArrayList();
            PSAccessRightsDescription psAccessRightsDescription;

            foreach (string permission in permissions)
            {
                if (!Enum.TryParse<PSAccessRightsDescription>(permission.Trim(), true, out psAccessRightsDescription))
                {
                    throw new ArgumentException("Invalid access policy permission");
                }
                else
                {
                    accessRights.Add(psAccessRightsDescription.ToString());
                }
            }

            try
            {
                ProvisioningServiceDescription provisioningServiceDescription = GetIotDpsResource(this.ResourceGroupName, this.Name);
                IList<SharedAccessSignatureAuthorizationRuleAccessRightsDescription> iotDpsAccessPolicyList = GetIotDpsAccessPolicy(this.ResourceGroupName, this.Name);

                SharedAccessSignatureAuthorizationRuleAccessRightsDescription iotDpsAccessPolicy = GetIotDpsAccessPolicy(this.ResourceGroupName, this.Name, this.KeyName);

                foreach (SharedAccessSignatureAuthorizationRuleAccessRightsDescription accessPolicy in iotDpsAccessPolicyList)
                {
                    if (accessPolicy.KeyName.Equals(iotDpsAccessPolicy.KeyName))
                    {
                        accessPolicy.Rights = string.Join(", ", accessRights.ToArray());
                    }
                }

                provisioningServiceDescription.Properties.AuthorizationPolicies = iotDpsAccessPolicyList;
                IotDpsCreateOrUpdate(this.ResourceGroupName, this.Name, provisioningServiceDescription);

                this.WriteObject(IotDpsUtils.ToPSSharedAccessSignatureAuthorizationRuleAccessRightsDescription(GetIotDpsAccessPolicy(this.ResourceGroupName, this.Name, this.KeyName)), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

