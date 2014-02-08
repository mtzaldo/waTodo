using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace TodoMVP.Data
{
    class TodoRepository
    {
        static TodoRepository _db;
        static object _syncRoot = new object();
        string _conn;

        public TodoRepository(string conn)
        {
            _conn = conn;
        }

        public static TodoRepository Instance
        {
            get { return _db; }
        }

        public static void Create(string conn)
        {

            if (_db == null)
            {
                lock (_syncRoot)
                {
                    if (_db == null)
                        _db = new TodoRepository(conn);
                }
            }
        }

        public List<Models.TodoItem> SelectAllTodoItems()
        {
            string q = "SELECT * FROM TodoList";
            List<Models.TodoItem> items = null;

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                items = conn.Query<Models.TodoItem>(q).ToList();
            }

            return items;
        }

        public Models.TodoItem SelectTodoItemById(int id)
        {
            string q = "SELECT * FROM TodoList WHERE Id=@Id";
            Models.TodoItem i = null;

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                i = conn.Query<Models.TodoItem>(q, new { Id = id }).Single();
            }

            return i;
        }

        public void InsertTodoItem(Models.TodoItem item)
        {
            string q = "INSERT INTO TodoList(Description, Done) VALUES(@Description, @Done)";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                conn.Execute(q, item);
            }
        }

        public void UpdateTodoItem(Models.TodoItem item)
        {
            string q = "UPDATE TodoList SET Description=@Description, Done=@Done WHERE Id=@Id";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                conn.Execute(q, item);
            }
        }

        public void DeleteTodoItemById(int id)
        {
            string q = "DELETE FROM TodoList WHERE Id=@Id";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                conn.Execute(q, new { Id = id });
            }
        }


    }
}
