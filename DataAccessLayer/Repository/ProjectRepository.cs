using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Core.Models;

namespace DataAccessLayer.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ClaimRequestDbContext db;
        public ProjectRepository()
        {
            db = new ClaimRequestDbContext();
        }
        public void Add(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Project Find(Guid projectId)
        {
            return db.Projects.Find(projectId);
        }

        public IList<Project> GetAll()
        {
            return db.Projects.ToList();
        }

        public string GetApproverId(string projectId)
        {
            throw new NotImplementedException();
        }

        public IList<Project> GetByStaffId(string StaffId)
        {
            return db.Projects.Where(p => p.StaffProjectMaps.Any(sp => sp.StaffId.Equals(StaffId))).ToList();
        }

        public void Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
