using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.Notification
{
    /// <summary>
    /// The animation properties for a notification message or some
    /// other item.
    /// </summary>
    public interface INotificationAnimation
    {
        /// <summary>
        /// Set DismissAnimation
        /// </summary>
        bool DismissAnimation { get; set; }

        /// <summary>
        /// Set StartAnimation
        /// </summary>
        bool StartAnimation { get; set; }

        /// <summary>
        /// Gets or sets whether the item animates in and out.
        /// </summary>
        bool Animates { get; set; }

        /// <summary>
        /// Gets the animatable UIElement.
        /// Typically this is the whole Control object so that the entire
        /// item can be animated.
        /// </summary>
        INotificationAnimation AnimatableElement { get; }
    }
}