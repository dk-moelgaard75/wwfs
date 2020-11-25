using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelLog.Models
{
    public class VehicleModel
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }
        
        [JsonProperty(PropertyName = "userid")]
        public String UserId { get; set; }

        [JsonProperty(PropertyName = "vehiclebrand")]
        public String VehicleBrand { get; set; }

        [JsonProperty(PropertyName = "vehiclemodelname")]
        public String VehicleModelName { get; set; }

        [JsonProperty(PropertyName = "licenseplate")]
        public String LicensePlate { get; set; }


        public string GetVehicleIdentification
        {
            get
            {
                return VehicleBrand + " - " + VehicleModelName + " (" + LicensePlate + ")";
            }
        }
    }
}
