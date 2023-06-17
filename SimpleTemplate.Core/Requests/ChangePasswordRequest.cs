using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTemplate.Application.Requests
{
    public class ChangePasswordRequest
    {
        public ChangePasswordRequest(string userName, string currentPassword, string newPassword)
        {
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
