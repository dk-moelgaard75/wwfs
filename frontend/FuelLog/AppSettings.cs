using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelLog
{
    public class AppSettings
    {
        public string UserCreateURL { get; set; }
        public string UserGetURL { get; set; }
        public string UserUpdateURL { get; set; }
        public string UserDeleteURL { get; set; }

        public string VehicleCreateURL { get; set; }
        public string VehicleGetURL { get; set; }
        public string VehicleUpdateURL { get; set; }
        public string VehicleDeleteURL { get; set; }

        public string FuelingCreateURL { get; set; }
        public string FuelingGetURL { get; set; }
        public string FuelingUpdateURL { get; set; }
        public string FuelingDeleteURL { get; set; }
    }
}
