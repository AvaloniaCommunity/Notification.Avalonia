using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Avalonia.Notification;

public class NotificationStyle : AvaloniaObject, IStyle, IResourceProvider
{
    private readonly IStyle _controlsStyles;

    private CancellationTokenSource? _currentCancellationTokenSource;
    private Task? _currentThemeUpdateTask;
    private bool _isLoading;
    private IStyle? _loaded;


    public NotificationStyle(Uri? baseUri)
    {
        _controlsStyles = new StyleInclude(baseUri)
        {
            Source = new Uri("avares://Notification.Avalonia/Themes/Generic.xaml")
        };
    }


    public NotificationStyle(IServiceProvider serviceProvider)
        : this(((IUriContext)serviceProvider.GetService(typeof(IUriContext)))?.BaseUri)
    {
    }

    /// <summary>
    /// Gets the loaded style.
    /// </summary>
    public IStyle Loaded
    {
        get
        {
            if (_loaded != null) return _loaded!;
            _isLoading = true;

            _loaded = new Styles() { _controlsStyles };

            _isLoading = false;

            return _loaded!;
        }
    }


    public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

    bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;

    public event EventHandler OwnerChanged
    {
        add
        {
            if (Loaded is IResourceProvider rp)
            {
                rp.OwnerChanged += value;
            }
        }
        remove
        {
            if (Loaded is IResourceProvider rp)
            {
                rp.OwnerChanged -= value;
            }
        }
    }

    public bool TryGetResource(object key, out object? value)
    {
        if (!_isLoading && Loaded is IResourceProvider p)
        {
            return p.TryGetResource(key, out value);
        }

        value = null;
        return false;
    }

    void IResourceProvider.AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
    void IResourceProvider.RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);
    IReadOnlyList<IStyle> IStyle.Children => _loaded?.Children ?? Array.Empty<IStyle>();

    public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);
}