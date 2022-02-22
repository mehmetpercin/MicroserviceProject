namespace MicroservisProject.Web.CustomExceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException()
        {
        }

        public UnAuthorizeException(string? message) : base(message)
        {
        }

        public UnAuthorizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
