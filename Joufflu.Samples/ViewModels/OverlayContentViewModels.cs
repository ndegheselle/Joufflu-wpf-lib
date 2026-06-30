using CommunityToolkit.Mvvm.ComponentModel;

namespace Joufflu.Samples.ViewModels;

/// <summary>Simple overlay content showing a message.</summary>
public class ConfirmViewModel : ObservableObject
{
    public ConfirmViewModel(string message) => Message = message;

    public string Message { get; }
}

/// <summary>Overlay content with an editable field, used by the form overlay demo.</summary>
public class SampleFormViewModel : ObservableObject
{
    private string _name = "Ada Lovelace";
    private bool _subscribe = true;

    public string Name { get => _name; set => SetProperty(ref _name, value); }

    public bool Subscribe { get => _subscribe; set => SetProperty(ref _subscribe, value); }
}
