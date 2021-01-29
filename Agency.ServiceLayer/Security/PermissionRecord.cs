using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ServiceLayer.Security
{
    public class PermissionRecord
    {
        public string RoleName { get; set; }
        public IEnumerable<PermissionModel> Permissions { get; set; }
    }
}
