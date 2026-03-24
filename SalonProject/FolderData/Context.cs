using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonProject.FolderData
{
    public partial class SalonEntities: DbContext
    {
        private static SalonEntities context;

        public static SalonEntities GetContext()
        {
            if (context == null)
            {
                context = new SalonEntities();
            }
            return context;
        }
    }
}
