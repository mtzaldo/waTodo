using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVP.Views;

namespace TodoMVP.Presenters
{
    public class Todo
    {
        ITodo _view = null;

        public Todo(ITodo view)
        {
            _view = view;
        }

        public void Initialize()
        {
        }

        public void GetTodoList(bool refresh = false)
        {
            _view.TodoList = Data.TodoRepository.Instance.SelectAllTodoItems();
        }

        public Models.TodoItem GetTodoListItemById(int id)
        {
            return  Data.TodoRepository.Instance.SelectAllTodoItems().Single(i=>i.Id==id);
        }

        public void InsertTodoItem(Models.TodoItem item)
        {
            if (item != null && item.IsValid())
                Data.TodoRepository.Instance.InsertTodoItem(item);
            else
                if (_view.OnOperationExecuted != null)
                    _view.OnOperationExecuted(
                        this, 
                        new Events.OperationResultEventArgs(
                            false, 
                            "insert", 
                            "please provide a valid description"));
        }

        public void UpdateTodoItem(Models.TodoItem item)
        {
            if (item != null && item.IsValid())
                Data.TodoRepository.Instance.UpdateTodoItem(item);
            else
                if (_view.OnOperationExecuted != null)
                    _view.OnOperationExecuted(
                        this, 
                        new Events.OperationResultEventArgs(
                            false, 
                            "update", 
                            "please provide a valid description"));
        }

        public void DeleteTodoItem(int id)
        {
            if (id > 0)
                Data.TodoRepository.Instance.DeleteTodoItemById(id);
            else
                if (_view.OnOperationExecuted != null)
                    _view.OnOperationExecuted(
                        this, 
                        new Events.OperationResultEventArgs(
                            false, 
                            "delete", 
                            "the item was not found"));
        }
    }
}
