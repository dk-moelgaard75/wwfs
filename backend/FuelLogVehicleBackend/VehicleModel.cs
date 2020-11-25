using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FuelLogVehicleBackend
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid UserId { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModelName { get; set; }
        public string LicensePlate { get; set; }
        [NotMapped]
        public bool DataIsValid { get; set; }
        [NotMapped]
        public string InvalidReason { get; set; }
    }
}
