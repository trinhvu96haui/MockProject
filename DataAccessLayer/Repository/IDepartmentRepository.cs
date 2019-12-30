using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Core.Models;

namespace DataAccessLayer.Repository
{
    public interface IDepartmentRepository : IDisposable
    {
        //void Add(Department department);
        //void Upadte(Department department);
        //void Delete(Department department);
        //void Delete(Guid departmentId);
        IList<Department> GetAll();
    }
}
