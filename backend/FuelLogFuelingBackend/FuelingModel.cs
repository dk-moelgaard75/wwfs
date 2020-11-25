using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FuelLogFuelingBackend
{
    public class FuelingModel
    {
        [Key]
        public int Id { get; set; }

        public Guid VehicleId { get; set; }
        
        public int Milages { get; set; }
        
        public double FuelAmount { get; set; }
        
        public DateTime FuelingDate { get; set; }
        [NotMapped]
        public bool DataIsValid { get; set; }
        [NotMapped]
        public string InvalidReason { get; set; }

    }
}
