﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SV20T1020570.BusinessLayers;

namespace SV20T1020570.Web
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> Provinces()
        {
            List<SelectListItem > list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn tỉnh/thành --"
            });
            foreach (var item in CommonDataService.ListOfProvinces())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ProvinceName, 
                    Text = item.ProvinceName
                });
            }
            return list;
        }
       
        
    }
}
