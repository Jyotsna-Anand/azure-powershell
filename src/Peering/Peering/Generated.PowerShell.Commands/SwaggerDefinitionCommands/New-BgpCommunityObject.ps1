<#
Code generated by Microsoft (R) PSSwagger 0.3.0
Changes may cause incorrect behavior and will be lost if the code is regenerated.
#>

<#
.SYNOPSIS
    The peering prefix class.

.DESCRIPTION
    The peering prefix class.

.PARAMETER Community
    Gets or sets the community.

.PARAMETER Metro
    Gets or sets the metro.

#>
function New-BgpCommunityObject
{
    param(    
        [Parameter(Mandatory = $false)]
        [string]
        $Community,
    
        [Parameter(Mandatory = $false)]
        [string]
        $Metro
    )
    
    $Object = New-Object -TypeName Microsoft.PowerShell.AzPeering.v001.Models.BgpCommunity

    $PSBoundParameters.GetEnumerator() | ForEach-Object { 
        if(Get-Member -InputObject $Object -Name $_.Key -MemberType Property)
        {
            $Object.$($_.Key) = $_.Value
        }
    }

    if(Get-Member -InputObject $Object -Name Validate -MemberType Method)
    {
        $Object.Validate()
    }

    return $Object
}