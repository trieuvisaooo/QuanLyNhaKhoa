using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace QuanLyNhaKhoa.ViewModels.Notification
{
    public class NotificationList
    {
        public ObservableCollection<NotificationBadge> Notifications;
        public Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;
        private int shownSeconds = 2;

        public void WriteMesssage(string message, bool isBad = false)
        {
            NotificationBadge notificationBadge = new(message, false, shownSeconds);
            notificationBadge.DestroyEvent = Destroy;
            Notifications.Add(notificationBadge);
            Debug.WriteLine($"{message} is added");
        }

        public NotificationList ()
        {
            Notifications = new ObservableCollection<NotificationBadge>();
            dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        }

        public void Destroy(string Id)
        {
            NotificationBadge theBadge = null;
            lock (Notifications)
            {
                foreach (var badge in Notifications)
                {
                    if (badge.Id == Id)
                    {
                        theBadge = badge;
                    }
                }
                if (theBadge != null)
                {
                    Debug.WriteLine($"{theBadge.Message} is removed");
                    dispatcherQueue.TryEnqueue(() =>
                    {
                        Notifications.Remove(theBadge);
                    });
                } 
            }
        }
    }


}
