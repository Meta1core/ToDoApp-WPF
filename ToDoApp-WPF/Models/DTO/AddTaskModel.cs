using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_WPF.Models.DTO
{
    class AddTaskModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfTask { get; set; }
        public int? Directory_Id { get; set; }
    }
}
