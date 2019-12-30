using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Core.Models;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ClaimRequestDbContext db;
        public DepartmentRepository()
        {
            db = new ClaimRequestDbContext();
        }
        //public void Add(Department department)
        //{
        //    db.Entry(department).State = EntityState.Added;
        //    db.SaveChanges();
        //}
        //public void Delete(Guid departmentId)
        //{
        //    var item = db.Departments.Find(departmentId);
        //    db.Departments.Remove(item);
        //    db.SaveChanges();
        //}
        //public void Delete(Department department)
        //{
        //    var item = db.Departments.Find(department.DepartmentId);
        //    db.Departments.Remove(item);
        //    db.SaveChanges();
        //}
        public void Dispose()
        {
            db.Dispose();
        }
        public IList<Department> GetAll()
        {
            return db.Departments.ToList();
        }
        //public void Upadte(Department department)
        //{
        //    db.Entry(department).State = EntityState.Modified;
        //    db.SaveChanges();
        //}
    }
}
