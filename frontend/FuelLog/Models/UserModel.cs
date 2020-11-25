using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelLog.Models
{
    public class UserModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public String FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public String LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public String Email { get; set; }


        [JsonProperty(PropertyName = "userverified")]
        public bool UserVerified { get; set; }


        public string GetUserIdentification
        {
            get
            {
                return FirstName + " - " + LastName;
            }
        }

    }
}
