using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class RoleRequest
    {

        public string Name { get; set; } = string.Empty;

        public RoleRequest() { }

        public RoleRequest(string name)
        {
            Name = name;
        }
    }
}
