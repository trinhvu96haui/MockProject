using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ClaimRequest.Core.Models;

namespace DataAccessLayer.Repository
{
    public class ClaimInforRepository : IClaimInforRepository
    {
        private readonly ClaimRequestDbContext db;
        public ClaimInforRepository()
        {
            db = new ClaimRequestDbContext();
        }
        public void Add(ClaimInfor claimInfor)
        {
            db.ClaimInfors.Add(claimInfor);
            db.SaveChanges();
        }

        public void Delete(int claimInforId)
        {
            var item = db.ClaimInfors.Find(claimInforId);
            db.ClaimInfors.Remove(item);
            db.SaveChanges();
        }

        public void Delete(ClaimInfor claimInfor)
        {
            var item = db.ClaimInfors.Find(claimInfor.ClaimInforId);
            db.ClaimInfors.Remove(item);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IList<ClaimInfor> GetAll()
        {
            return db.ClaimInfors.ToList();
        }

        public IList<ClaimInfor> GetClaimInforByClaimId(Guid claimId)
        {
            return db.ClaimInfors.Where(c => c.ClaimId == claimId).ToList();
        }

        public void Update(ClaimInfor claimInfor)
        {
            db.Entry(claimInfor).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
