namespace Nabeghe.Application.Exceptions
{
	public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message,object key)
        :base($"{message} {key} was not found")
        {
            
        }
    }
}
