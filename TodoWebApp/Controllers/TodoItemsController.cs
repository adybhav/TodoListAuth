using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApp.Models;

using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace TodoWebApp.Controllers
{[Authorize]
    public class TodoItemsController : Controller
    {
        private readonly TodoDataContext _context;

        public TodoItemsController()
        {
            _context = new TodoDataContext();
        }

        // GET: TodoItems
        //public IActionResult Index(int del) {
        //    var List = _context.TodoItems.ToList();
        //    List = List.Where(m => m.Done).ToList();
        //    return View(List);
        //}
        public ActionResult DeleteCompleted() {
            var List = _context.TodoItems.ToList();
            List = List.Where(m => m.Done).ToList();
            foreach (var a in List)
            {
                _context.TodoItems.Remove(a);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Index(string filter)
        {
            var Username = User.Identity.GetUserName(); 
            var List = _context.TodoItems.Where(m => m.UserId == Username).ToList();

            if (!String.IsNullOrEmpty(filter))
            {
                if (filter.Equals("com"))
                {
                    List = List.Where(m => m.Done).ToList();


                    return View(List);
                }
                else if (filter.Equals("incom"))
                {
                    List = List.Where(m => !m.Done).ToList();

                    return View(List);

                }
            }
            return View(List);
        }
        // GET: TodoItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var todoItem = await _context.TodoItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }

            return View(todoItem);
        }

        // GET: odoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include ="Id,Value,Done")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.UserId = User.Identity.GetUserName();
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var todoItem = await _context.TodoItems.SingleOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include ="Id,Value,Done")] TodoItem todoItem)
        {
          

            if (id != todoItem.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var dbitem = _context.TodoItems.Find(id);
                    dbitem.LastAccessed = DateTime.Now;
                    dbitem.Value = todoItem.Value;
                    dbitem.Done = todoItem.Done;
                    if (todoItem.Done) {
                        dbitem.CompleteDate = DateTime.Now;
                        
                    }
                    

                    // TODO _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var todoItem = await _context.TodoItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }

            return View(todoItem);
        }

       
        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
