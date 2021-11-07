namespace Diploma.Views
{
    public class ErrorState
    {
        public ErrorState(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
