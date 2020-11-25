using FuelLog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelLog.Utility
{
    public static class ConverterUtil
    {
        public static List<SelectListItem> GetUsersAsSelect(List<UserModel> users, string user)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem() { Text = "Vælg", Value = "-" });
            foreach(UserModel model in users)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = model.GetUserIdentification,
                    Value = model.UserId.ToString()
                };
                if ( model.UserId.ToString().Equals(user))
                {
                    item.Selected = true;
                }

                newList.Add(item);
            }
            return newList;
        }
        public static List<SelectListItem> GetVehiclesAsSelect(List<VehicleModel> vehicles)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem() { Text = "Vælg", Value = "-" });
            foreach (VehicleModel model in vehicles)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = model.GetVehicleIdentification,
                    Value = model.UserId.ToString()
                };
                newList.Add(item);
            }
            return newList;
        }

    }
}
