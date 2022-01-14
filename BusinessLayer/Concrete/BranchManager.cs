using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserrrrrSon.Models.models_;

namespace BusinessLayer.Concrete
{
    public class BranchManager : IBranchService
    {
        IBranchDAL _BranchDAL;

        public BranchManager(IBranchDAL branchDAL)
        {
            _BranchDAL = branchDAL;
        }

        public async Task<List<Branch>> GetList()
        {
            return await _BranchDAL.GetListAll();
        }

        public Branch TAdd(Branch t)
        {
            return _BranchDAL.Insert(t);
        }

        public void TDelete(Branch t)
        {
            _BranchDAL.Delete(t);
        }

        public Branch TGetByID(int id)
        {
            return _BranchDAL.GetByID(id);
        }

        public Branch TUpdate(Branch t)
        {
            return _BranchDAL.Update(t);
        }
    }
}
