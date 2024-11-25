using Plugin.LocalNotification;

namespace SmartAgricultureApp_MAUI.Services;

public class NotificationPageService
{
    public void SendNotification(string title, string message)
    {
        var notification = new NotificationRequest
        {
            NotificationId = 1001,
            Title = title,
            Description = message,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(30)
            }
        };
        LocalNotificationCenter.Current.Show(notification);
    }
}