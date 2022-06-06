﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public List<int> Roles { get; set; }
    }
}
