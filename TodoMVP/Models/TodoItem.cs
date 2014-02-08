using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVP.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.Description);
        }
    }
}
