using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Timer = System.Threading.Timer;

namespace QuanLyNhaKhoa.ViewModels.Notification
{
    public class NotificationBadge : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _message;
        private bool _isBad;
        private bool _isStarted = false;
        public string Id = Guid.NewGuid().ToString();
        private int secondsTillDestroy = 2;
        public Action<string> DestroyEvent;
        private Timer _timer;
        


        public NotificationBadge(string message, bool isBad = false, int secondTillDestroy = 2)
        {
            Message = message;
            IsBad = isBad;
            this.secondsTillDestroy = secondTillDestroy;

            _timer = new Timer(TimerCallback, null, 0, 1000 * secondsTillDestroy);
        }

        public void ExtendLivingTime(int extraSeconds)
        {
            // Change the due time of the timer to extend its living time
            _timer.Change(1000 * extraSeconds, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            Debug.WriteLine("Callback called");
            if (!_isStarted)
            {
                _isStarted = true;
                return;
            }
            _timer.Dispose();
            DestroyEvent(this.Id);
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public bool IsBad
        {
            get
            {
                return _isBad;
            }

            set
            {
                _isBad = value;
                OnPropertyChanged(nameof(IsBad));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
