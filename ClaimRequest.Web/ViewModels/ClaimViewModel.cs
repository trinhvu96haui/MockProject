using ClaimRequest.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mock.Models.ViewModel
{
    public class ClaimViewModel
    {
        public Claim Claim { get; set; }
        public Staff Staff { get; set; }
        public string  ClaimAction { get; set; }
        public IList<SimpleProject> ListSimpleProject { get; set; }
        public List<Guid> ListClaimInfoError { get; set; }
        public ClaimViewModel()
        {
            Claim = new Claim();
            Staff = new Staff();
            ListSimpleProject = new List<SimpleProject>();
            Claim.ClaimInfors = new List<ClaimInfor>();
            ListClaimInfoError = new List<Guid>();
        }
    }
    public class SimpleProject
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string RoleInProject { get; set; }
        public string Duration { get; set; }
    }
}