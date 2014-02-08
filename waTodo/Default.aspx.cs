using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Events = TodoMVP.Events;
using Models = TodoMVP.Models;
using Presenters = TodoMVP.Presenters;
using Views = TodoMVP.Views;

namespace waTodo
{
    public partial class Default : System.Web.UI.Page, Views.ITodo
    {
        Presenters.Todo _p = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            _p = new Presenters.Todo(this);

            if (!this.IsPostBack)
                _p.Initialize();
        }

        protected void bntAdd_Click(object sender, EventArgs e)
        {
            Models.TodoItem i = new Models.TodoItem() { Description = this.txtDescription.Text };
           _p.InsertTodoItem(i);

           this.txtDescription.Text = string.Empty;
        }

        protected void grvTodo_PreRender(object sender, EventArgs e)
        {
            _p.GetTodoList();
        }

        protected void grvTodo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Models.TodoItem row = e.Row.DataItem as Models.TodoItem;

                if (row.Done) e.Row.Cells[1].Style.Add("text-decoration", "line-through");

                Button edit = e.Row.Cells[2].FindControl("btnEdit") as Button;
                edit.Visible = this.grvTodo.EditIndex == -1;

                Button save = e.Row.Cells[2].FindControl("btnSave") as Button;
                save.Visible = this.grvTodo.EditIndex >= 0;

                CheckBox cbDone = e.Row.Cells[0].FindControl("cbDone") as CheckBox;
                cbDone.InputAttributes.Add("item-id", row.Id.ToString());
                cbDone.Checked = row.Done;
            }
        }

        protected void grvTodo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)e.Keys["Id"];
            _p.DeleteTodoItem(id);
        }

        protected void grvTodo_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void grvTodo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CheckBox cbDone = this.grvTodo.Rows[e.RowIndex].FindControl("cbDone") as CheckBox;
            Models.TodoItem i = new Models.TodoItem();
            i.Id = (int)e.Keys["Id"];
            i.Description = e.NewValues["Description"] as string;
            i.Done = cbDone.Checked;//(bool)e.OldValues["Done"];

            _p.UpdateTodoItem(i);

            this.grvTodo.EditIndex = -1;
        }

        protected void cbDone_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbDone = sender as CheckBox;
            int id = Convert.ToInt32(cbDone.InputAttributes["item-id"]);
            Models.TodoItem item = _p.GetTodoListItemById(id);

            item.Done = cbDone.Checked;

            _p.UpdateTodoItem(item);

        }

        protected void OperationExecuted(object sender, Events.OperationResultEventArgs e)
        {
            if (!e.IsSuccessful)
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "operationError", 
                    string.Format("alert('{0} : {1}')", e.Operation, e.Message),
                    true);

        }

        //ITodo
        public List<Models.TodoItem> TodoList
        {
            set 
            { 
                this.grvTodo.DataSource = value;
                this.grvTodo.DataBind();
            }
        }

        EventHandler<Events.OperationResultEventArgs> _onOperationExecuted = null;
        public EventHandler<Events.OperationResultEventArgs> OnOperationExecuted
        {
            get 
            { 
                if (_onOperationExecuted == null)
                    _onOperationExecuted = new EventHandler<Events.OperationResultEventArgs>(OperationExecuted);

                return _onOperationExecuted;
            }
        }
    }
}