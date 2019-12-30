using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Core.Models;
using MockProjectCore.Constants;

namespace DataAccessLayer.Repository
{
    public class StaffProjectMapRepository : IStaffProjectMapRepository
    {
        private readonly ClaimRequestDbContext db;
        public StaffProjectMapRepository()
        {
            db = new ClaimRequestDbContext();
        }

        public List<Staff> GetStaffByProjejctId(Guid projectId, int jobRank)
        {
            var staffProjectMap = db.StaffProjectMaps.Where(p => p.ProjectId.Equals(projectId)).ToList();
            List<Staff> listStaff = new List<Staff>();
            foreach (var item in staffProjectMap)
            {
                Staff user = db.Users.Find(item.StaffId);
                if (user.JobRank == jobRank)
                {
                    listStaff.Add(user);
                }
            }
            return listStaff;
        }

        public List<StaffProjectMap> GetByStaffId(string StaffId)
        {
            var x = db.StaffProjectMaps.Include("Project").Where(m => m.StaffId.Equals(StaffId)).ToList();
            return db.StaffProjectMaps.Include("Project").Where(m => m.StaffId.Equals(StaffId)).ToList();
        }

        public string GetListStaffByProjectId(Guid projectId, int jobRank)
        {
            List<StaffProjectMap> staffProjectMap = db.StaffProjectMaps.Where(p => p.ProjectId.Equals(projectId)).ToList();
            string listStaff = "";
            foreach (var item in staffProjectMap)
            {
                var staff = db.Users.Find(item.StaffId);
                if (staff != null && staff.JobRank == jobRank)
                {
                    listStaff += item.Staff.StaffName + "; ";
                }
            }
            return listStaff;
        }

        public Staff GetPmOfProject(Guid projectId)
        {
            var x = db.StaffProjectMaps.FirstOrDefault(m => m.ProjectId.Equals(projectId) && m.RoleInProject == (int)JobRank.PM);
            return x.Staff;
        }

        public List<Guid> GetListProjectIdByPmId(string PmId)
        {
            return db.StaffProjectMaps.Where(m => m.StaffId.Equals(PmId) && m.RoleInProject == (int)JobRank.PM).Select(m => m.ProjectId).ToList();
        }

        public StaffProjectMap GetProjectByStaffPM(string staffId)
        {
            return db.StaffProjectMaps.FirstOrDefault(m => m.StaffId.Equals(staffId) && m.RoleInProject == (int)JobRank.PM);
        }

        public List<StaffProjectMap> GetListProjectByStaffPM(string staffId)
        {
            return db.StaffProjectMaps.Where(m => m.StaffId.Equals(staffId) && m.RoleInProject == (int)JobRank.PM).ToList();
        }
    }
}
