using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVP.Data
{
    public static class InitRepository
    {
        public static void Todo(string connectionString)
        {
            TodoRepository.Create(connectionString);
        }
    }
}
