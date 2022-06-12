using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLFile: DLBase<File>, IDLFile
    {
        public DLFile(WhereHouseContext context) : base(context)
        {
        }
    }
}
