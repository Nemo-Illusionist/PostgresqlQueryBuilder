namespace QueryBuilder.Exception
{
    public class OutOfSequenceException : System.Exception
    {
        public OutOfSequenceException()
        {
        }

        public OutOfSequenceException(string message) : base(message)
        {
        }

        public OutOfSequenceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}