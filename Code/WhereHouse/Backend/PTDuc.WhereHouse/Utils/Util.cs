﻿using System;
using System.Linq;

namespace PTDuc.WhereHouse.Utils
{
    public static class Util
    {
        public static T Map<T>(object objfrom, T objto)
        {
            var ToProperties = objto.GetType().GetProperties();
            var FromProperties = objfrom.GetType().GetProperties();

            ToProperties.ToList().ForEach(o =>
            {
                var fromp = FromProperties.FirstOrDefault(x => x.Name == o.Name && x.PropertyType == o.PropertyType);
                if (fromp != null)
                {
                    o.SetValue(objto, fromp.GetValue(objfrom));
                }
            });

            return objto;
        }

        
    }
}
