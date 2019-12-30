using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockProjectCore;
using MockProjectCore.Constants;
using System.Data.Entity;
using ClaimRequest.Core.Models;

namespace DataAccessLayer.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ClaimRequestDbContext db;
        public ClaimRepository()
        {
            db = new ClaimRequestDbContext();
        }
        public void Add(Claim claim)
        {
            claim.CreateDate = DateTime.Now;
            db.Claims.Add(claim);
            db.SaveChanges();
        }
        public void Approve(Claim claim)
        {
            claim.Status = (int)ClaimStatus.Approved;
            claim.ApproveDate = DateTime.Now;
            db.Entry(claim).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Cancel(Claim claim)
        {
            claim.Status = (int)ClaimStatus.Cancelled;
            db.Entry(claim).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public IList<Claim> Find(int claimId)
        {
            throw new NotImplementedException();
        }
        public IList<Claim> GetAll()
        {
            return db.Claims.ToList();
        }

        public IList<Claim> GetAllApproved()
        {
            return db.Claims.Include("Staff").Include("Project").Where(c => c.Status == (int)ClaimStatus.Approved).ToList();
        }

        public IList<Claim> GetAllApprovedByUserId(string userId)
        {
            return db.Claims.Include("Staff").Include("Project").Where(c => c.Status == (int)ClaimStatus.Approved && c.StaffId == userId).ToList();
        }

        public IList<Claim> GetAllCancelled()
        {
            return db.Claims.Where(c => c.Status == (int)ClaimStatus.Cancelled).ToList();
        }

        public IList<Claim> GetAllDraftByUserId(string userId)
        {
            return db.Claims.Where(c => c.Status == (int)ClaimStatus.Draft && c.StaffId == userId).ToList();
        }

        public IList<Claim> GetAllPaid()
        {
            return db.Claims.Include("Staff").Include("Project").Where(c => c.Status == (int)ClaimStatus.Paid).ToList();
        }

        public IList<Claim> GetAllPaidByUserId(string userId)
        {
            return db.Claims.Include("Staff").Include("Project").Where(c => c.Status == (int)ClaimStatus.Paid && c.StaffId == userId).ToList();
        }

        public IList<Claim> GetAllPendingApprovalByUserid(string userId)
        {
            return db.Claims.Where(c => c.Status == (int)ClaimStatus.PendingApproval && c.StaffId == userId).ToList();
        }

        public IList<Claim> GetAllPendingByProject(Guid projectId)
        {
            return db.Claims.ToList();
        }

        public IList<Claim> GetAllRejected()
        {
            return db.Claims.Where(c => c.Status == (int)ClaimStatus.Rejected).ToList();
        }

        public IList<Claim> GetAllRejectOrCancelledByUserId(string userId)
        {
            return db.Claims.Where(c => (c.Status == (int)ClaimStatus.Rejected || c.Status == (int)ClaimStatus.Cancelled) && c.StaffId == userId).ToList();
        }

        public IList<Claim> GetByCurrentMonth()
        {
            return db.Claims.ToList();
        }

        public void Paid(Claim claim)
        {
            claim.Status = (int)ClaimStatus.Paid;
            claim.PaidDate = DateTime.Now;
            db.Entry(claim).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Reject(Claim claim)
        {
            claim.Status = (int)ClaimStatus.Rejected;
            db.Entry(claim).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Return(Claim claim)
        {
            claim.Status = (int)ClaimStatus.Draft;
            db.Entry(claim).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Update(Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}
