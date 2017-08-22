using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebApp.Models
{
    public class TodoUserInfo
    {
        public string Email { get; set; }
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Incomplete { get; set; }
    }
}