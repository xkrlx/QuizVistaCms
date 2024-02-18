using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class ToggleRoleRequest
    {
        public string UserName {  get; set; }
        public string RoleName { get; set; }

        public ToggleRoleRequest() { }

        public ToggleRoleRequest(string UserName, string RoleName)
        {
            this.UserName = UserName;
            this.RoleName = RoleName;
        }

    }
}
