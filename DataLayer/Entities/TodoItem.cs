using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public Priority Priorities { get; set; }
        public string Name { get; set; }
        public bool isCompleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedTime { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
       public User User { get; set; }
       public enum Priority
        {
            Low=1,
            Medium=2,
            High=3,
        }
    }
}
