using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IStaffRepository:IDisposable
    {
        void Add(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
        IList<Staff> GetAll();
        IList<Staff> GetPM();
        IList<Staff> GetQA();
        IList<Staff> GetTechnicalLead();
        IList<Staff> GetBA();
        IList<Staff> GetDeveloper();
        IList<Staff> GetTester();
        IList<Staff> GetTechnicalConsultancy();
        IList<Staff> GetFinances();
        IList<Staff> GetApprovers();
        Staff Find(string userId);
    }
}
