using System;

namespace BenfordSet.Common
{
    internal class Validation
    {
        public event EventHandler? ObjectIsNull;
        public event EventHandler? IsCanceld;

        internal object Object { get; private set; }
        internal Messages Messages { get; set; }
        internal Validation() 
        {
            Messages Messages = new();
            ObjectIsNull += Messages.Validation_ObjectIsNull;
        }

        internal bool IsObjectNull(object obj)
        {
            Object = obj;
            if (Object == null)
            {
                ObjectIsNull?.Invoke(this, EventArgs.Empty);
                return false;
            }
            else
                return true;
        }
    }
}
