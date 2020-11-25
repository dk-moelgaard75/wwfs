using System;
using System.Collections.Generic;
using System.Text;

namespace FuelLogUserBackend
{
    public static class UserModelFactory
    {
        public static UserModel CreateUserModel(dynamic data)
        {
            if (String.IsNullOrEmpty((string)data.firstname))
            {
                return new UserModel() { DataIsValid = false, InvalidReason = "Missing FirstName" };
            }
            if (String.IsNullOrEmpty((string)data.lastname))
            {
                return new UserModel() { DataIsValid = false, InvalidReason = "Missing LastName" };
            }
            if (String.IsNullOrEmpty((string)data.email))
            {
                return new UserModel() { DataIsValid = false, InvalidReason = "Missing Email" };
            }

            return new UserModel {
                FirstName = data.firstname,
                LastName = data.lastname,
                Email = data.email,
                UserId = Guid.NewGuid(),
                DataIsValid = true,
                UserVerified = false };
        }
        public static void UpdateUserModel(dynamic data, ref UserModel user)
        {
            user.FirstName = data.firstname;
            user.LastName = data.lastname;
            user.Email = data.email;
        }
    }
}
