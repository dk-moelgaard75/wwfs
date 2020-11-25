using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelLog.Models
{
    public class FuelingModel
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }
        
        [JsonProperty(PropertyName = "vehicleid")]
        public Guid VehicleId { get; set; }

        [JsonProperty(PropertyName = "milages")]
        public int Milages { get; set; }

        [JsonProperty(PropertyName = "fuelamount")]
        public double FuelAmount { get; set; }

        [JsonProperty(PropertyName = "fuelingdate")]
        public DateTime FuelingDate { get; set; }
    }
}
