using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVP.Views
{
    public interface ITodo
    {
        List<Models.TodoItem> TodoList { set; }
        EventHandler<Events.OperationResultEventArgs> OnOperationExecuted { get; }
    }
}
