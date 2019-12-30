using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaimRequest.Core.Models;
using DataAccessLayer.Repository;
using Mock.Models.ViewModel;
using Microsoft.AspNet.Identity;
using MockProjectCore.Constants;
using System.Web.UI.WebControls;

namespace ClaimRequest.Web.Controllers
{
    public class ClaimsController : Controller
    {
        private ClaimRequestDbContext db = new ClaimRequestDbContext();
        private readonly IClaimRepository _claimRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStaffProjectMapRepository _staffProjectMapRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ClaimsController(IClaimRepository claimRepository, IStaffRepository staffRepository, IProjectRepository projectRepository, IStaffProjectMapRepository staffProjectMapRepository, IDepartmentRepository departmentRepository)
        {
            _claimRepository = claimRepository;
            _staffRepository = staffRepository;
            _projectRepository = projectRepository;
            _staffProjectMapRepository = staffProjectMapRepository;
            _departmentRepository = departmentRepository;
        }
        // GET: Claims/Create
        public ActionResult Create()
        {
            ClaimViewModel model = new ClaimViewModel();
            GetRelatData(model);
            ViewBag.header = "Create claim";
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(ClaimViewModel model)
        {
            RePairClaim(model.Claim);
            //var validateWithProject = model.Claim.ValidateDateClaimInfoWithProject();
            //var validateWithClient = model.Claim.CheckClaimClient();
            //var validateWithServer = model.Claim.CheckClaimServer();
            //if (validateWithProject.Count > 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Claim information must in range startdate - enddate");
            //    model.ListClaimInfoError = model.ListClaimInfoError.Union(validateWithProject).ToList();
            //}

            //if (validateWithClient.Count > 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Claim information time range is not valid");
            //    model.ListClaimInfoError = model.ListClaimInfoError.Union(validateWithClient).ToList();
            //}

            //if (validateWithServer.Count > 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Duplicated Claim !There is already claim in this time range.");
            //    model.ListClaimInfoError = model.ListClaimInfoError.Union(validateWithServer).ToList();
            //}

            //if (model.Claim.claimInfos.Count == 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Please add some claim information");
            //}

            if (ModelState.IsValid)
            {
                Claim claim = new Claim()
                {
                    ClaimId = model.Claim.ClaimId,
                    AuditTrail = model.Claim.AuditTrail,
                    ProjectId = model.Claim.ProjectId,
                    Remark = model.Claim.Remark,
                    Status = model.Claim.Status,
                    StaffId = model.Claim.StaffId,
                    TotalWorkingHour = model.Claim.TotalWorkingHour,
                    ClaimInfors = model.Claim.ClaimInfors
                };
                claim.AuditTrail = claim.AuditTrail ?? "";
                if (model.ClaimAction.Equals("Submit"))
                {
                    claim.Status = (int)ClaimStatus.PendingApproval;
                    if (claim.ClaimId == Guid.Empty)
                    {
                        //submit at first time
                        claim.AuditTrail += "\nSubmitted on << " + DateTime.Now + " >> by << " + User.Identity.GetUserName() + " >>";
                        _claimRepository.Add(claim);
                    }
                    else
                    {
                        //submit at update
                        claim.AuditTrail += "\nUpdated on << " + DateTime.Now + " >> by << " + User.Identity.GetUserName() + " >>";
                        claim.AuditTrail += "\nSubmitted on << " + DateTime.Now + " >> by << " + User.Identity.GetUserName() + " >>";
                        _claimRepository.Update(claim);
                    }
                    //EmailSender.SendReminderAsync(claim);
                }
                else
                {
                    if (claim.ClaimId == Guid.Empty)
                    {
                        //save at first time
                        claim.AuditTrail += "Created on << " + DateTime.Now + " >> by << " + User.Identity.GetUserName() + " >>";
                        _claimRepository.Add(claim);
                    }
                    else
                    {
                        //save at update
                        claim.AuditTrail += "\nUpdated on << " + DateTime.Now + " >> by << " + User.Identity.GetUserName() + " >>";
                        _claimRepository.Update(claim);
                    }
                }
                return Json(new { IsSuccess = true, ClaimId = claim.ClaimId });
            }
            GetRelatData(model);
            model.Claim.Status = (int)ClaimStatus.Draft;
            //return PartialView("_PartialFormSaveClaim", model);
            return View("Create");

        }




        [NonAction]
        public void GetRelatData(ClaimViewModel model)
        {
            var staffId = model.Claim.ClaimId.Equals(Guid.Empty) ? User.Identity.GetUserId() : model.Claim.StaffId;
            var varProjectList = _staffProjectMapRepository.GetByStaffId(staffId);
            model.Staff = _staffRepository.Find(staffId);
            var dic = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(JobRank)))
            {
                dic.Add((int)item, Enum.GetName(typeof(JobRank), (int)item));
            }
            foreach (var item in varProjectList)
            {
                model.ListSimpleProject.Add(new SimpleProject()
                {
                    ProjectId = item.ProjectId,
                    ProjectName = item.Project.ProjectName,
                    Duration = item.Project.StartDate.ToShortDateString() + " - " + item.Project.EndDate.ToShortDateString(),
                    RoleInProject = dic[item.RoleInProject]
                });
            }
        }
        // GET: Claims
        public ActionResult Index() 
        {
            return View("Welcome");
        }

        // GET: Claims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

       

        // POST: Claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ClaimId,Remark,TotalWorkingHour,AuditTrail,StaffId,Status,CreateDate,ApproveDate,PaidDate,ProjectId")] Claim claim)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Claims.Add(claim);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectCode", claim.ProjectId);
        //    ViewBag.StaffId = new SelectList(db.Users, "Id", "StaffName", claim.StaffId);
        //    return View(claim);
        //}

        // GET: Claims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectCode", claim.ProjectId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "StaffName", claim.StaffId);
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClaimId,Remark,TotalWorkingHour,AuditTrail,StaffId,Status,CreateDate,ApproveDate,PaidDate,ProjectId")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectCode", claim.ProjectId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "StaffName", claim.StaffId);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Claim claim = db.Claims.Find(id);
            db.Claims.Remove(claim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult GetModalAddInfor(ClaimInforViewModel model)
        {
            ModelState.Clear();
            if (model.ClaimInfoId == null)
            {
                return PartialView("_ModalAddClaimInfo", new ClaimInforViewModel());
            }
            return PartialView("_ModalAddClaimInfo");
        }

        [HttpPost]
        public ActionResult ValidateModal(ClaimInforViewModel model)
        {
            if (DateTime.Compare(model.ClaimInfoFrom, model.ClaimInfoTo) >= 0)
            {
                ModelState.AddModelError(string.Empty, "\"To\" time must be greater than \"From\"");
            }
            if (model.TotalHour <= 0)
            {
                ModelState.AddModelError("TotalHour", "Total hour must greater than zero !");
            }
            var realTotal = (decimal)(model.ClaimInfoTo - model.ClaimInfoFrom).TotalHours;
            if (model.TotalHour > realTotal)
            {
                ModelState.AddModelError("TotalHour", "Total hour must equal or less than " + realTotal + " !");
            }
            if (ModelState.IsValid)
            {
                return Json(new
                {
                    IsSuccess = true,
                    data = new
                    {
                        IsNew = model.ClaimInfoId == null,
                        ClaimInfoId = model.ClaimInfoId ?? Guid.NewGuid().ToString(),
                        Day = model.ClaimInfoDate.Value.DayOfWeek.ToString(),
                        ClaimInfoDate = model.ClaimInfoDate.Value.ToShortDateString(),
                        ClaimInfoFrom = model.ClaimInfoFrom.ToString("HH:mm"),
                        ClaimInfoTo = model.ClaimInfoTo.ToString("HH:mm"),
                        TotalHour = model.TotalHour,
                        Remark = model.Remark
                    }
                });
            }
            return PartialView("_ModalAddClaimInfo", model);
        }

        [NonAction]
        public void RePairClaim(Claim claim)
        {
            foreach (var item in claim.ClaimInfors)
            {
                item.From = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, item.From.Hour, item.From.Minute, item.From.Second);
                item.To = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, item.To.Hour, item.To.Minute, item.To.Second);
            }
        }
    }
}
