using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MockProjectCore.DAL
{
    public class DBInitializer:DropCreateDatabaseAlways<ClaimDbContext>
    {
        protected override void Seed(ClaimDbContext context)
        {
            base.Seed(context);
        }
    }
}
