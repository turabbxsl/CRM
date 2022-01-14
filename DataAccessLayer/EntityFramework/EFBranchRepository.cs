using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserrrrrSon.Models.models_;

namespace DataAccessLayer.EntityFramework
{
   public class EFBranchRepository:GenericRepository<Branch>,IBranchDAL
    {
    }
}
