using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IStaffProjectMapRepository
    {
        List<StaffProjectMap> GetByStaffId(string StaffId);
        List<Guid> GetListProjectIdByPmId(string PmId);
        List<Staff> GetStaffByProjejctId(Guid projectId, int jobRank);
        StaffProjectMap GetProjectByStaffPM(string staffId);
        List<StaffProjectMap> GetListProjectByStaffPM(string staffId);
        Staff GetPmOfProject(Guid projectId);
        string GetListStaffByProjectId(Guid projectId, int jobRank);
    }
}
