using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVP.Events
{
    public class OperationResultEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public string Operation { get; set; }
        public string Message { get; set; }

        public OperationResultEventArgs(bool success, string operation, string message)
        {
            this.IsSuccessful = success;
            this.Operation = operation;
            this.Message = message;
        }
    }
}
