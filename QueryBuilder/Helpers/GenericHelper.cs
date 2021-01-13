using System.Runtime.InteropServices;

namespace QueryBuilder.Helpers
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct EmptyGeneric<T> 
    {
    }

    public class GenericHelper
    {
        internal GenericHelper()
        {
        }

        public EmptyGeneric<T> Empty<T>()
        {
            return new();
        }
    }
}