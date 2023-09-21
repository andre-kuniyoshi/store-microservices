
namespace EventBus.Messages.Events
{
    public class RegisterNewUserEvent : IntegrationBaseEvent
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
