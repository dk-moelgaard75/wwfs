using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogVehicleBackend
{
    public static class VehicleModelFactory
    {
        public static VehicleModel CreateVehicleModel(dynamic data)
        {
            if (String.IsNullOrEmpty((string)data.userid))
            {
                return new VehicleModel() { DataIsValid = false, InvalidReason = "Missing UserId" };
            }
            if (String.IsNullOrEmpty((string)data.vehiclebrand))
            {
                return new VehicleModel() { DataIsValid = false, InvalidReason = "Missing VehicleBrand" };
            }
            if (String.IsNullOrEmpty((string)data.vehiclemodelname))
            {
                return new VehicleModel() { DataIsValid = false, InvalidReason = "Missing VehicleModelName" };
            }
            if (String.IsNullOrEmpty((string)data.licenseplate))
            {
                return new VehicleModel() { DataIsValid = false, InvalidReason = "Missing LicensPlate" };
            }

            return new VehicleModel {
                UserId = data.userid,
                VehicleBrand = data.vehiclebrand,
                VehicleModelName = data.vehiclemodelname,
                LicensePlate = data.licenseplate,
                VehicleId = Guid.NewGuid(),
                DataIsValid = true
            };
        }
        public static void UpdateUserModel(dynamic data, ref VehicleModel vehicle)
        {
            vehicle.LicensePlate = data.licenseplate;
            vehicle.VehicleModelName = data.vehiclemodelname;
            vehicle.VehicleBrand = data.vehiclebrand;
        }
    }
}
