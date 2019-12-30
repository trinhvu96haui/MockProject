using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IClaimInforRepository : IDisposable
    {
        void Add(ClaimInfor claimInfor);
        void Update(ClaimInfor claimInfor);
        void Delete(ClaimInfor claimInfor);
        void Delete(int claimInforId);
        IList<ClaimInfor> GetAll();
        IList<ClaimInfor> GetClaimInforByClaimId(Guid claimId);
    }
}
