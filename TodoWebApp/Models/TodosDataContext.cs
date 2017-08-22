using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TodoWebApp.Models
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext()
            : base("DefaultConnection")
        {
            
        }
        public DbSet<TodoItem> TodoItems { get; set; }

     
    }

    public class TodoItem
    {   
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Value { get; set; }
        [DisplayName("Completed?")]
        public bool Done { get; set; }
        [DisplayName("Created At")]
        public DateTime WhenCreated { get; set; }
        [DisplayName("Last Modified")]
        public DateTime? LastAccessed { get; set; }
        [DisplayName("Complete Date")]
        public DateTime? CompleteDate { get; set; }
        [DisplayName("Email Address")]
        public string UserId { get; set; }
        public TodoItem()
        {
            Done = false;
            WhenCreated = DateTime.Now;
        }

    }
}