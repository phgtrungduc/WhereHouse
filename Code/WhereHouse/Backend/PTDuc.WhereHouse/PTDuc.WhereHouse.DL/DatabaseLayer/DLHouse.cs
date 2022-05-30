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

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLHouse : DLBase<House>, IDLHouse
    {
        public DLHouse(WhereHouseContext context) : base(context)
        {
        }

        public IEnumerable<House> GetDeepData()
        {
            _dbSet = _context.Set<House>();
            return _dbSet.Include(x => x.UserOwner);
        }
    }
}
