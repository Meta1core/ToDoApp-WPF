using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_WPF.Models.DTO;

namespace ToDoApp_WPF.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Header { get; set; }

        public bool IsFavorite { get; set; }

        public virtual User User { get; set; }

        public virtual Directory Directory { get; set; }

        public bool IsDone { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime? DateOfTask { get; set; }

        public bool IsNotReadOnly { get { return !IsDone; } }
    }
}
