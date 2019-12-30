using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MockProjectCore.Constants;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClaimRequest.Core.Models
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ClaimRequestDbInitializer : DropCreateDatabaseIfModelChanges<ClaimRequestDbContext>
    {
        protected override void Seed(ClaimRequestDbContext context)
        {
            Department department1 = new Department()
            {
                DepartmentName = "IT"
            };
            Department department2 = new Department()
            {
                DepartmentName = "Hr"
            };
            context.Departments.Add(department1);
            context.Departments.Add(department2);
            context.SaveChanges();

            InitializeIdentity(context);
            InitProject(context);
            InitStaffProjectMap(context);
            InitClaim(context);
            base.Seed(context);
        }
        //seed data for Claim
        public static void InitClaim(ClaimRequestDbContext db)
        {
            var roleClaimer = db.Roles.FirstOrDefault(r => r.Name.Equals("Claimer")).Id;
            var user = db.Users.FirstOrDefault(u => u.Roles.Any(r => r.RoleId.Equals(roleClaimer)));
            var project = db.StaffProjectMaps.FirstOrDefault(spm => spm.StaffId.Equals(user.Id)).Project;
            ClaimInfor infor1 = new ClaimInfor()
            {
                Date = DateTime.Now,
                From = DateTime.Now,
                To = new DateTime(2020,12,12),
                Remarks = "Remark1",
                TotalNoOfHours = 1
            };

            ClaimInfor infor2 = new ClaimInfor()
            {
                Date = DateTime.Now,
                From = DateTime.Now,
                To = new DateTime(2020, 11, 11),
                Remarks = "Remark2",
                TotalNoOfHours = 2
            };
            ClaimInfor infor3 = new ClaimInfor()
            {
                Date = DateTime.Now,
                From = DateTime.Now,
                To = new DateTime(2020, 10, 10),
                Remarks = "Remark3",
                TotalNoOfHours = 3
            };

            ClaimInfor infor4 = new ClaimInfor()
            {
                Date = DateTime.Now,
                From = DateTime.Now,
                To = new DateTime(2020, 09, 09),
                Remarks = "Remark4",
                TotalNoOfHours = 4
            };
            ClaimInfor infor5 = new ClaimInfor()
            {
                Date = DateTime.Now,
                From = DateTime.Now,
                To = new DateTime(2020, 08, 08),
                Remarks = "Remark5",
                TotalNoOfHours = 5
            };
            Claim claim1 = new Claim()
            {
                Project = project,
                Status = (int)ClaimStatus.Draft,
                Remark = "Remark1",
                StaffId = user.Id,
                ClaimInfors = new List<ClaimInfor>()
                {
                    infor1,
                    infor2
                }
            };
            Claim claim2 = new Claim()
            {
                Project = project,
                Status = (int)ClaimStatus.Approved,
                Remark = "Remark1",
                StaffId = user.Id,
                ClaimInfors = new List<ClaimInfor>()
                {
                    infor3,
                    infor4
                }
            };

            Claim claim3 = new Claim()
            {
                Project = project,
                Status = (int)ClaimStatus.PendingApproval,
                Remark = "Remarks",
                StaffId = user.Id,
                ClaimInfors = new List<ClaimInfor>()
                {
                    infor5                 
                }
            };
            db.Claims.Add(claim1);
            db.Claims.Add(claim2);
            db.Claims.Add(claim3);
            db.SaveChanges();
        }

        //seed data for Project
        public static void InitProject(ClaimRequestDbContext db)
        {
            Project project1 = new Project()
            {
                ProjectCode = "ProjectCode_BS",
                ProjectName = "Project Book store",
                StartDate = new DateTime(2018, 09, 05),
                EndDate = new DateTime(2020, 09, 05)
            };
            Project project2 = new Project()
            {
                ProjectCode = "ProjectCode_JB",
                ProjectName = "Project Just Blog",
                StartDate = new DateTime(2019, 06, 05),
                EndDate = new DateTime(2020, 09, 05)
            };
            db.Projects.Add(project1);
            db.Projects.Add(project2);
            db.SaveChanges();
        }

        //seed data for StaffProjectMap
        public static void InitStaffProjectMap(ClaimRequestDbContext db)
        {
            var listProject = db.Projects.ToList();
            var listPM = db.Users.Where(u => u.JobRank == (int)JobRank.PM).ToList();
            var listBA = db.Users.Where(u => u.JobRank == (int)JobRank.BA).ToList();
            var listQA = db.Users.Where(u => u.JobRank == (int)JobRank.QA).ToList();
            var listDeveloper = db.Users.Where(u => u.JobRank == (int)JobRank.Developers).ToList();
            var listTester = db.Users.Where(u => u.JobRank == (int)JobRank.Testers).ToList();
            var listTechnicalConsultancy = db.Users.Where(u => u.JobRank == (int)JobRank.TechnicalConsultancy).ToList();
            var listTechnicalLead = db.Users.Where(u => u.JobRank == (int)JobRank.TechnicalLead).ToList();

            StaffProjectMap mapPM1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listPM[0].Id,
                RoleInProject = listPM[0].JobRank
            };
           
            StaffProjectMap mapBA1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listBA[0].Id,
                RoleInProject = listBA[0].JobRank
            };
            StaffProjectMap mapBA2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listBA[1].Id,
                RoleInProject = listBA[1].JobRank
            };
            StaffProjectMap mapQA1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listQA[0].Id,
                RoleInProject = listQA[0].JobRank
            };
            StaffProjectMap mapQA2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listQA[1].Id,
                RoleInProject = listQA[1].JobRank
            };
            StaffProjectMap mapDEV1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listDeveloper[0].Id,
                RoleInProject = listDeveloper[0].JobRank
            };
            StaffProjectMap mapDEV2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listDeveloper[1].Id,
                RoleInProject = listDeveloper[1].JobRank
            };
            StaffProjectMap mapTEST1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTester[0].Id,
                RoleInProject = listTester[0].JobRank
            };
            StaffProjectMap mapTEST2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTester[1].Id,
                RoleInProject = listTester[1].JobRank
            };
            StaffProjectMap mapTL1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTechnicalLead[0].Id,
                RoleInProject = listTechnicalLead[0].JobRank
            };
            StaffProjectMap mapTL2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTechnicalLead[1].Id,
                RoleInProject = listTechnicalLead[1].JobRank
            };
            StaffProjectMap mapTC1 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTechnicalConsultancy[0].Id,
                RoleInProject = listTechnicalConsultancy[0].JobRank
            };
            StaffProjectMap mapTC2 = new StaffProjectMap()
            {
                ProjectId = listProject[0].ProjectId,
                StaffId = listTechnicalConsultancy[1].Id,
                RoleInProject = listTechnicalConsultancy[1].JobRank
            };
            db.StaffProjectMaps.Add(mapBA1);
            db.StaffProjectMaps.Add(mapBA2);
            db.StaffProjectMaps.Add(mapPM1);
            //db.StaffProjectMaps.Add(mapPM2);

            db.StaffProjectMaps.Add(mapQA1);
            db.StaffProjectMaps.Add(mapQA2);
            db.StaffProjectMaps.Add(mapDEV1);
            db.StaffProjectMaps.Add(mapDEV2);

            db.StaffProjectMaps.Add(mapTEST1);
            db.StaffProjectMaps.Add(mapTEST2);

            db.StaffProjectMaps.Add(mapTL1);
            db.StaffProjectMaps.Add(mapTL2);
            db.StaffProjectMaps.Add(mapTC1);
            db.StaffProjectMaps.Add(mapTC2);
            db.SaveChanges();
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentity(ClaimRequestDbContext db)
        {
            var userManager = new UserManager<Staff>(new UserStore<Staff>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //const string name = "admin@example.com";           
            //const string roleName = "Admin";

            const string password = "Admin@123456";
            var departmentDemo = db.Departments.FirstOrDefault();
            string[] roles = new string[] { "Administrator", "Finance", "Approver", "Claimer" };

            //Create Role Admin if it does not exist
            foreach (var item in roles)
            {
                var role = roleManager.FindByName(item);
                if (role == null)
                {
                    role = new IdentityRole(item);
                    var roleResult = roleManager.Create(role);
                }
            }

            var admins = new[]
            {
                new {name = "vutv1_admin",email="admin1@gmail.com"},
                new {name = "vutv2_admin",email ="admin2@gmail.com"},
                new {name = "vutv3_admin",email = "admin3@gmail.com"}
            };
            var finances = new[]
            {
                new {name = "vutv1_finance",email="finance1@gmail.com"},
                new {name = "vutv2_finance",email="finance2@gmail.com"},
                new {name = "vutv3_finance",email="finance3@gmail.com"}
            };
            var appovers = new[]
            {
                new {name = "vutv1_appover",email = "appover1@gmail.com"},
                new {name = "vutv2_appover",email = "appover2@gmail.com"},
                new {name = "vutv3_appover",email = "appover3@gmail.com"}
            };
            var claimer = new[]
           {
                new {name="vutv1_BA",email="claimer1@mail.com",jobRank=(int)JobRank.BA},
                new {name="vutv2_BA",email="claimer2@mail.com",jobRank=(int)JobRank.BA},

                new {name="vutv1_QA",email="claimer3@mail.com",jobRank=(int)JobRank.QA},
                new {name="vutv2_QA",email="claimer4@mail.com",jobRank=(int)JobRank.QA},

                new {name="vutv1_Develope",email="claimer5@mail.com",jobRank=(int)JobRank.Developers},
                new {name="vutv2_Develope",email="claimer6@mail.com",jobRank=(int)JobRank.Developers},

                new {name="vutv1_Tester",email="claimer7@mail.com",jobRank=(int)JobRank.Testers},
                new {name="vutv2_Tester",email="claimer8@mail.com",jobRank=(int)JobRank.Testers},

                new {name="vutv1_TechnicalConsultancy",email="claimer9@mail.com",jobRank=(int)JobRank.TechnicalConsultancy},
                new {name="vutv2_TechnicalConsultancy",email="claimer10@mail.com",jobRank=(int)JobRank.TechnicalConsultancy},

                new {name="vutv1_TechnicalLead",email="claimer11@mail.com",jobRank=(int)JobRank.TechnicalLead},
                new {name="vutv2_TechnicalLead",email="claimer12@mail.com",jobRank=(int)JobRank.TechnicalLead},
            };

            // add user with role admin
            foreach (var item in admins)
            {
                var user = userManager.FindByName(item.email);
                if (user == null)
                {
                    user = new Staff { UserName = item.email, Email = item.email, StaffName = item.name, JobRank = (int)JobRank.None, Department = departmentDemo, Salary = 500000 };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }
                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains("Administrator"))
                {
                    var result = userManager.AddToRole(user.Id, "Administrator");
                }
            }

            //add user with role finance
            foreach (var item in finances)
            {
                var user = userManager.FindByName(item.email);
                if (user == null)
                {
                    user = new Staff { UserName = item.email, Email = item.email, StaffName = item.name, JobRank = (int)JobRank.None, Department = departmentDemo, Salary = 20000000 };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }
                //add user finance to role finance if not added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains("Finance"))
                {
                    var result = userManager.AddToRole(user.Id, "Finance");
                }
            }
            //add user with role approver
            foreach (var item in appovers)
            {
                var user = userManager.FindByName(item.email);
                if (user == null)
                {
                    user = new Staff { UserName = item.email, Email = item.email, StaffName = item.name, JobRank = (int)JobRank.PM, Department = departmentDemo, Salary = 20000000 };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }
                //add user approver to role approver if not added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains("Approver"))
                {
                    var result = userManager.AddToRole(user.Id, "Approver");
                }
            }

            //add claimer with role claimer
            foreach (var item in claimer)
            {
                var user = userManager.FindByName(item.email);
                if (user == null)
                {
                    user = new Staff { UserName = item.email, Email = item.email, StaffName = item.name, JobRank = item.jobRank, Department = departmentDemo, Salary = 20000000 };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }
                //add user claimer to role claimer if not added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains("Claimer"))
                {
                    var result = userManager.AddToRole(user.Id, "Claimer");
                }
            }
        }
    }
}
