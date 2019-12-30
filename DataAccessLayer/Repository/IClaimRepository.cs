using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IClaimRepository:IDisposable
    {
        void Add(Claim claim);
        void Update(Claim claim);
        IList<Claim> GetAll();
        void Approve(Claim claim);
        void Paid(Claim claim);
        void Reject(Claim claim);
        void Return(Claim claim);
        void Cancel(Claim claim);
        IList<Claim> Find(int claimId);
        IList<Claim> GetAllDraftByUserId(string userId);
        IList<Claim> GetAllPendingApprovalByUserid(string userId);
        IList<Claim> GetAllApprovedByUserId(string userId);
        IList<Claim> GetAllApproved();
        IList<Claim> GetAllPaid();
        IList<Claim> GetAllPaidByUserId(string userId);
        IList<Claim> GetByCurrentMonth();
        IList<Claim> GetAllRejected();
        IList<Claim> GetAllCancelled();
        IList<Claim> GetAllRejectOrCancelledByUserId(string userId);
        IList<Claim> GetAllPendingByProject(Guid projectId);
    }
}
