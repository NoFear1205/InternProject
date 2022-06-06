using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class ChangePasswordRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string rePassword { get; set; }
    }
}
