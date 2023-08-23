namespace Register.Application.NotificationPattern
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void AddNotication(Notification notification);
    }
}
