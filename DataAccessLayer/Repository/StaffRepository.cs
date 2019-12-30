using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Core.Models;
using MockProjectCore.Constants;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ClaimRequestDbContext db;
        public StaffRepository()
        {
            db = new ClaimRequestDbContext();
        }
        public void Add(Staff staff)
        {
            db.Users.Add(staff);
            db.SaveChanges();
        }

        public void Delete(Staff staff)
        {
            var item = db.Users.Find(staff.Id);
            db.Users.Remove(item);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        public Staff Find(string userId)
        {
            return db.Users.Include(s => s.Department).FirstOrDefault(s => s.Id.Equals(userId));
        }
        public IList<Staff> GetAll()
        {
            return db.Users.Include("Department").ToList();
        }

        public IList<Staff> GetApprovers()
        {
            var role = db.Roles.FirstOrDefault(r => r.Name.Equals("Approver"));
            return db.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals(role.Id))).ToList();
        }

        public IList<Staff> GetBA()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.BA).ToList();
        }

        public IList<Staff> GetDeveloper()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.Developers).ToList();
        }

        public IList<Staff> GetFinances()
        {
            throw new NotImplementedException();
        }

        public IList<Staff> GetPM()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.PM).ToList();
        }

        public IList<Staff> GetQA()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.QA).ToList();
        }

        public IList<Staff> GetTechnicalConsultancy()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.TechnicalConsultancy).ToList();
        }

        public IList<Staff> GetTechnicalLead()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.TechnicalLead).ToList();
        }

        public IList<Staff> GetTester()
        {
            return db.Users.Where(s => s.JobRank == (int)JobRank.Testers).ToList();
        }

        public void Update(Staff staff)
        {
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
