using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.UserDTO
{
    public class ProfileDTO
    {
        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }

        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }
    }
}
