using PTDuc.WhereHouse.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLAddress
    {
        List<Province> GetProvinces(string filePath);
        List<District> GetDistricts(string filePath);
        List<District> GetDistrictsByParent(string rootFilePath, string parentCode);
        List<Ward>  GetWards(string filePath);
        List<Ward> GetWardsByParent(string rootFilePath, string parentCode);
    }
}
