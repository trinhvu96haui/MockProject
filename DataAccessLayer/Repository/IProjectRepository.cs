using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IProjectRepository:IDisposable
    {
        Project Find(Guid projectId);
        string GetApproverId(string projectId);
        void Add(Project project);
        void Update(Project project);
        IList<Project> GetByStaffId(string StaffId);
        IList<Project> GetAll();
    }
}
