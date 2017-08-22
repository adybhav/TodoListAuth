using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Models;

using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace TodoWebApp.Controllers
{   [Authorize]
    public class AdminController : Controller
    {
        private readonly TodoDataContext _context;

        public AdminController()
        {
            _context = new TodoDataContext();
        }

     
        public ActionResult Index(string filter)
        {
            if (User.Identity.GetUserName() != "Admin@login.com")
            {
                return RedirectToAction("Index","Home");
            }
       
            ApplicationDbContext adc = new ApplicationDbContext();
            var users = adc.Users.ToList();
            var list = new List<TodoUserInfo>(); 
            foreach (var a in users) {
                TodoUserInfo tdui = new TodoUserInfo();
                tdui.Email = a.Email;
                var useritems = _context.TodoItems.Where(m => m.UserId == a.Email).ToList();
                tdui.Count=useritems.Count;
                tdui.Completed = useritems.Where(m => m.Done).Count();
                tdui.Incomplete = tdui.Count - tdui.Completed ;
                list.Add(tdui);
                
            }
            return View(list);
        }
        
    }
}
