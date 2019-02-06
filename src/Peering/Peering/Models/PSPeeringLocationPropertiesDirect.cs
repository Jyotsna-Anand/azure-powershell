// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.PowerShell.Cmdlets.Peering.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The peering location properties class.
    /// </summary>
    public partial class PSPeeringLocationPropertiesDirect
    {
        /// <summary>
        /// Initializes a new instance of the PSPeeringLocationPropertiesDirect
        /// class.
        /// </summary>
        public PSPeeringLocationPropertiesDirect()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PSPeeringLocationPropertiesDirect
        /// class.
        /// </summary>
        /// <param name="directPeeringFacilities">Gets or sets the peering
        /// facilities available at the location.</param>
        /// <param name="bandwidthOffers">Gets or sets the bandwidth offers
        /// available at the location.</param>
        public PSPeeringLocationPropertiesDirect(IList<PSDirectPeeringFacility> directPeeringFacilities = default(IList<PSDirectPeeringFacility>), IList<PSPeeringBandwidthOffer> bandwidthOffers = default(IList<PSPeeringBandwidthOffer>))
        {
            DirectPeeringFacilities = directPeeringFacilities;
            BandwidthOffers = bandwidthOffers;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the peering facilities available at the location.
        /// </summary>
        [JsonProperty(PropertyName = "directPeeringFacilities")]
        public IList<PSDirectPeeringFacility> DirectPeeringFacilities { get; set; }

        /// <summary>
        /// Gets or sets the bandwidth offers available at the location.
        /// </summary>
        [JsonProperty(PropertyName = "bandwidthOffers")]
        public IList<PSPeeringBandwidthOffer> BandwidthOffers { get; set; }

    }
}