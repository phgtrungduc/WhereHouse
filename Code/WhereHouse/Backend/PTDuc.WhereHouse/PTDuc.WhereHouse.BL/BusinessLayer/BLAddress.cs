using Newtonsoft.Json;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLAddress : IBLAddress
    {
        public List<Province> GetProvinces(string filePath)
        {
            List<Province> res = null;
            if (!string.IsNullOrEmpty(filePath))
            {
                res = this.ReadFileJson<Province>(filePath);
            }
            return res;
        }
        public List<District> GetDistricts(string filePath)
        {
            List<District> res = null;
            if (!string.IsNullOrEmpty(filePath))
            {
                res = this.ReadFileJson<District>(filePath);
            }
            return res;
        }

        public List<Ward> GetWards(string filePath)
        {
            List<Ward> res = null;
            if (!string.IsNullOrEmpty(filePath))
            {
                res = this.ReadFileJson<Ward>(filePath);
            }
            return res; 
        }

        public List<District> GetDistrictsByParent(string rootFilePath, string parentCode)
        {
            List<District> res = null;
            if (!string.IsNullOrEmpty(parentCode))
            {
                var filePath = Path.Combine(rootFilePath, parentCode + ".json");
                if (File.Exists(filePath))
                {
                    res = this.ReadFileJson<District>(filePath);
                }
            }
            return res;
        }

        public List<Ward> GetWardsByParent(string rootFilePath, string parentCode)
        {
            List<Ward> res = null;
            if (!string.IsNullOrEmpty(parentCode))
            {
                var filePath = Path.Combine(rootFilePath, parentCode + ".json");
                if (File.Exists(filePath))
                {
                    res = this.ReadFileJson<Ward>(filePath);
                }
            }
            return res;
        }

        public List<TType> ReadFileJson<TType>(string filePath)
        {
            List<TType> res = null;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, TType>>(json);
                    res = data.Select(x => x.Value).ToList();
                }
            }
            return res;
        }
    }
}
