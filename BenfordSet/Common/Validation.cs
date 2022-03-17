using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Common
{
    internal class Validation
    {
        public event EventHandler? ObjectIsNull;
        public event EventHandler? IsCanceld;

        internal Object Object { get; private set; }
        internal Messages Messages { get; set; }
        internal Validation() 
        {
            Messages Messages = new();
            ObjectIsNull += Messages.Validation_ObjectIsNull;
        }
        internal Validation(object obj)
        {
            //Messages Messages = new();
            //ObjectIsNull += Messages.Validation_ObjectIsNull;
            //IsObjectNull();
        }

        internal bool IsObjectNull(object obj)
        {
            Object = obj;
            if (Object == null)
            {
                OnObjectIsNull();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnObjectIsNull()
        {
            ObjectIsNull?.Invoke(this, EventArgs.Empty);
        }
    }
}
