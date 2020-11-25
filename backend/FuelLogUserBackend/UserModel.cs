using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FuelLogUserBackend
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool UserVerified { get; set; }
        [NotMapped]
        public bool DataIsValid { get; set; }
        [NotMapped]
        public string InvalidReason { get; set; }
    }
}
