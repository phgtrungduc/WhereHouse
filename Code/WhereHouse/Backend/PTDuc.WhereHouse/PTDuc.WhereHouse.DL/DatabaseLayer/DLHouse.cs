using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLHouse : DLBase<House, HouseDTO>, IDLHouse
    {
        public DLHouse(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<House> GetDeepData()
        {
            _dbSet = _context.Set<House>();
            return _dbSet.Include(x => x.UserOwner);
        }

    }
}
