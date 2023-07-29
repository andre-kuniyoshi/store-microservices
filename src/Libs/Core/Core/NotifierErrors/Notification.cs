
namespace Core.NotifierErrors
{
    public class Notification
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public Notification(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
