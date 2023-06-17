using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTemplate.Application.Requests
{
    public class AddRolesRequest
    {
        public AddRolesRequest(string userName, IEnumerable<string> roles)
        {
            UserName = userName;
            Roles = roles;
        }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
