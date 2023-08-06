using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia.Notification.Samples.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        /// <summary>
        /// Gets the notification message manager.
        /// </summary>
        /// <value>
        /// The notification message manager.
        /// </value>
        public INotificationMessageManager Manager { get; } = new NotificationMessageManager();
        
        public void  ButtonBaseErrorOnClick()
        {
            this.Manager
                .CreateMessage()
                .Accent("#F15B19")
                .Background("#F15B19")
                .HasHeader("Lost connection to server")
                .HasMessage("Reconnecting...")
                .WithOverlay(new ProgressBar
                {
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 3,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)),
                    Background = Brushes.Transparent,
                    IsIndeterminate = true,
                    IsHitTestVisible = false
                })
                .Queue();
        }

        public void ButtonBaseWarningOnClick()
        {
            this.Manager
                .CreateMessage()
                .Accent("#E0A030")
                .Background("#333")
                .HasBadge("Warn")
                .HasHeader("Error")
                .HasMessage("Failed to retrieve data.")
                .WithButton("Try again", async button => { })
                .Dismiss().WithButton("Ignore", button => { })
                .Queue();
        }

        public void ButtonBaseInfoOnClick()
        {
            this.Manager
                .CreateMessage()
                .Accent("#1751C3")
                .Background("#333")
                .HasBadge("Info")
                .HasMessage("Update will be installed on next application restart.")
                .Dismiss().WithButton("Update now", button => { })
                .Dismiss().WithButton("Release notes", button => { })
                .Dismiss().WithButton("Later", button => { })
                .Queue();
        }

        public void ButtonBaseInfoDelayOnClick()
        {
            this.Manager
                .CreateMessage()
                .Accent("#1751C3")
                .Animates(true)
                .Background("#333")
                .HasBadge("Info")
                .HasMessage(
                    "Update will be installed on next application restart. This message will be dismissed after 5 seconds.")
                .Dismiss().WithButton("Update now", button => { })
                .Dismiss().WithButton("Release notes", button => { })
                .Dismiss().WithDelay(TimeSpan.FromSeconds(5))
                .Queue();
        }
        public string Greeting => "Welcome to Avalonia!";
    }
}