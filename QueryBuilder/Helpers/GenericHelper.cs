using System.Runtime.InteropServices;

namespace QueryBuilder.Helpers
{
    public interface IEmptyGeneric<T>
    {
    }

    public class GenericHelper
    {
        internal GenericHelper()
        {
        }

        [StructLayout(LayoutKind.Auto)]
        private readonly struct EmptyGeneric<T> : IEmptyGeneric<T>
        {
        }

        public IEmptyGeneric<T> Empty<T>()
        {
            return new EmptyGeneric<T>();
        }
    }
}