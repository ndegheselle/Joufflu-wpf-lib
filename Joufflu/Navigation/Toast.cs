using Joufflu.Assets.Fonts;
using Joufflu.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Joufflu.Navigation;

public enum ToastType
{
    Info,
    Success,
    Warning,
    Danger
}

/// <summary>Description of a toast to display.</summary>
public class ToastOptions
{
    public ToastType Type { get; set; } = ToastType.Info;

    public string Title { get; set; } = "";

    public string Message { get; set; } = "";

    /// <summary>Time before the toast auto-dismisses. <see cref="TimeSpan.Zero"/> keeps it sticky.</summary>
    public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(5);
}

/// <summary>
/// A live toast hosted by the <see cref="ToastService"/>.
/// </summary>
public class ToastInstance : ObservableObject
{
    private readonly ToastService _service;
    private readonly DispatcherTimer? _timer;

    public ToastType Type { get; }

    public string Title { get; }

    public string Message { get; }

    public bool HasTitle => !string.IsNullOrEmpty(Title);

    /// <summary>Lucide glyph representing the toast type.</summary>
    public string Icon => Type switch
    {
        ToastType.Success => LucideFontIcons.BadgeCheck,
        ToastType.Warning => LucideFontIcons.BadgeAlert,
        ToastType.Danger => LucideFontIcons.CircleX,
        _ => LucideFontIcons.Info
    };

    public ICommand CloseCommand { get; }

    public ToastInstance(ToastOptions options, ToastService service)
    {
        _service = service;
        Type = options.Type;
        Title = options.Title;
        Message = options.Message;

        CloseCommand = new RelayCommand(() => _service.Close(this));

        if (options.Duration > TimeSpan.Zero)
        {
            _timer = new DispatcherTimer { Interval = options.Duration };
            _timer.Tick += OnTick;
            _timer.Start();
        }
    }

    private void OnTick(object? sender, EventArgs e)
    {
        StopTimer();
        _service.Close(this);
    }

    internal void StopTimer()
    {
        if (_timer == null)
            return;

        _timer.Stop();
        _timer.Tick -= OnTick;
    }
}

/// <summary>
/// Default <see cref="IToastService"/> implementation. Newest toast is inserted on top.
/// </summary>
public class ToastService : ObservableObject, IToastService
{
    public ObservableCollection<ToastInstance> Toasts { get; } = new();

    public ToastInstance Show(ToastOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var instance = new ToastInstance(options, this);
        Toasts.Insert(0, instance);
        return instance;
    }

    public ToastInstance Info(string message, string title = "")
        => Show(new ToastOptions { Type = ToastType.Info, Message = message, Title = title });

    public ToastInstance Success(string message, string title = "")
        => Show(new ToastOptions { Type = ToastType.Success, Message = message, Title = title });

    public ToastInstance Warning(string message, string title = "")
        => Show(new ToastOptions { Type = ToastType.Warning, Message = message, Title = title });

    public ToastInstance Error(string message, string title = "")
        => Show(new ToastOptions { Type = ToastType.Danger, Message = message, Title = title });

    public void Close(ToastInstance toast)
    {
        toast.StopTimer();
        Toasts.Remove(toast);
    }
}
