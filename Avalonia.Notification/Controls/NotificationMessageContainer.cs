using System.Runtime.CompilerServices;
using Avalonia.Animation;
using Avalonia.Collections;
using Avalonia.Controls;

namespace Avalonia.Notification.Controls
{
    /// <summary>
    /// The notification message container.
    /// </summary>
    /// <seealso cref="ItemsControl" />
    public class NotificationMessageContainer : ItemsControl
    {
        /// <summary>
        /// Gets or sets the manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public INotificationMessageManager Manager
        {
            get => (INotificationMessageManager)this.GetValue(ManagerProperty);
            set => this.SetValue(ManagerProperty, value);
        }

        /// <summary>
        /// The manager property.
        /// </summary>
        public static readonly StyledProperty<NotificationMessageManager> ManagerProperty =
            AvaloniaProperty.Register<NotificationMessageContainer, NotificationMessageManager>("Manager");


        //TODO
        /// <summary>
        /// Managers the property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="NullReferenceException">Dependency object is not of valid type - expected NotificationMessageContainer.</exception>
        private static void ManagerPropertyChangedCallback(IAvaloniaObject dependencyObject,
            AvaloniaPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is NotificationMessageContainer @this))
                throw new NullReferenceException("Dependency object is not of valid type " +
                                                 nameof(NotificationMessageContainer));

            if (dependencyPropertyChangedEventArgs.OldValue is INotificationMessageManager oldManager)
                @this.DetachManagerEvents(oldManager);

            if (dependencyPropertyChangedEventArgs.NewValue is INotificationMessageManager newManager)
                @this.AttachManagerEvents(newManager);
        }

        /// <summary>
        /// Attaches the manager events.
        /// </summary>
        /// <param name="newManager">The new manager.</param>
        private void AttachManagerEvents(INotificationMessageManager newManager)
        {
            newManager.OnMessageQueued += ManagerOnOnMessageQueued;
            newManager.OnMessageDismissed += ManagerOnOnMessageDismissed;
        }

        /// <summary>
        /// Detaches the manager events.
        /// </summary>
        /// <param name="oldManager">The old manager.</param>
        private void DetachManagerEvents(INotificationMessageManager oldManager)
        {
            oldManager.OnMessageQueued -= ManagerOnOnMessageQueued;
            oldManager.OnMessageDismissed -= ManagerOnOnMessageDismissed;
        }

        /// <summary>
        /// Manager on message dismissed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotificationMessageManagerEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException">Can't use both ItemsSource and Items collection at the same time.</exception>
        private void ManagerOnOnMessageDismissed(object sender, NotificationMessageManagerEventArgs args)
        {
            /*if (this.Items != null)
                throw new InvalidOperationException(
                    "Can't use both ItemsSource and Items collection at the same time.");*/

            this.RemoveMessage(args.Message);
        }

        private void RemoveMessage(INotificationMessage message)
        {
            (this.Items as AvaloniaList<object>).Remove(message);
        }

        /// <summary>
        /// Manager on message queued.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotificationMessageManagerEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException">Can't use both ItemsSource and Items collection at the same time.</exception>
        private void ManagerOnOnMessageQueued(object sender, NotificationMessageManagerEventArgs args)
        {
            /*if (this.Items != ture)
                throw new InvalidOperationException(
                    "Can't use both ItemsSource and Items collection at the same time.");*/

            (this.Items as AvaloniaList<object>).Add(args.Message);

            if (args.Message is INotificationAnimation animatableMessage)
            {
                if (animatableMessage.Animates)
                {
                    animatableMessage.AnimatableElement.StartAnimation = true;
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="NotificationMessageContainer"/> class.
        /// </summary>
        static NotificationMessageContainer()
        {
            ManagerProperty.Changed.Subscribe(x => ManagerPropertyChangedCallback(x.Sender, x));
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationMessageContainer), new FrameworkPropertyMetadata(typeof(NotificationMessageContainer)));
        }
    }
}