using System;

namespace BenfordSet.Common
{
    internal class Validation
    {
        public event EventHandler? ObjectIsNull;

        internal object Object { get; private set; }
        internal Messages? Messages { get => new(); }
        internal Validation() 
        {
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
