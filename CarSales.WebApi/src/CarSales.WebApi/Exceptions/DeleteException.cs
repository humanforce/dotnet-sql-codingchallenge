namespace CarSales.WebApi.Exceptions
{
    public class DeleteException : InvalidOperationException
    {
        public DeleteException() { }

        public DeleteException(string message) : base(message) { }
    }
}
