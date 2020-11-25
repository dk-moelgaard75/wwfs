using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogFuelingBackend
{
    class FuelingModelFactory
    {
        public static FuelingModel CreateFuelingModel(dynamic data)
        {
            if (String.IsNullOrEmpty((string)data.vehicleid))
            {
                return new FuelingModel() { DataIsValid = false, InvalidReason = "Missing VehicleId" };
            }
            if (String.IsNullOrEmpty((string)data.milages))
            {
                return new FuelingModel() { DataIsValid = false, InvalidReason = "Missing Milages" };
            }
            if (String.IsNullOrEmpty((string)data.fuelamount))
            {
                return new FuelingModel() { DataIsValid = false, InvalidReason = "Missing Fuelamount" };
            }

            return new FuelingModel
            {
                VehicleId = Guid.Parse((string)data.vehicleid),
                Milages = (int)data.milages,
                FuelAmount = (double)data.fuelamount,
                FuelingDate = DateTime.Parse((string)data.fuelingdate),
                DataIsValid = true
            };
        }
        public static void UpdateUserModel(dynamic data, ref FuelingModel fueling)
        {
            fueling.Milages = (int)data.milage;
            fueling.FuelAmount = (double)data.fuelamount;
            fueling.FuelingDate = DateTime.Parse(data.fuelingdata);
        }
    }
}