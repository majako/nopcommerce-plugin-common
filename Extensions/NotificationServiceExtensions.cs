using Nop.Services.Messages;

namespace Majako.Plugin.Common.Extensions
{
    public static class NotificationServiceExtensions
    {
        public static void SuccessNotification(
            this INotificationService notificationService, 
            string pluginSystemName, 
            string message, 
            bool encode = true
            )
        {
            notificationService.SuccessNotification($"{pluginSystemName}: {message}", encode);
        }
        
        public static void WarningNotification(
            this INotificationService notificationService, 
            string pluginSystemName, 
            string message, 
            bool encode = true
        )
        {
            notificationService.WarningNotification($"{pluginSystemName}: {message}", encode);
        }
        
        public static void ErrorNotification(
            this INotificationService notificationService, 
            string pluginSystemName, 
            string message, 
            bool encode = true
        )
        {
            notificationService.ErrorNotification($"{pluginSystemName}: {message}", encode);
        }
    }
}