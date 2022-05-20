using System;

namespace BenfordSet.Common
{
    internal class Validation
    {
        internal object Object { get; private set; }

        public event EventHandler? ObjectIsNull;
        internal bool IsObjectNull(object obj)
        {
            Object = obj;
            if (Object == null)
            {
                return false;
            }
            else
                return true;
        }
    }
}
