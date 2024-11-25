using MauiAppBtgPactual.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MauiAppBtgPactual.ViewModels;

public class ClienteViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ClienteModel>? Cliente { get; set; }

    private Guid _id;
    private string? _name;
    private string? _lastName;
    private string? _age;
    private string? _address;

    public Guid ID
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }
    public string? Name
    {
        get => _name;
        set
        {
            _name = Regex.Match(value, @"[a-zA-Z\s]+").Value;
            OnPropertyChanged();
        }
    }
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = Regex.Match(value, @"^[a-zA-Z][a-zA-Z\s]+").Value;
            OnPropertyChanged();
        }
    }
    public string? Age
    {
        get => _age;
        set
        {
            _age = Regex.Replace(value, @"\D+", "");
            OnPropertyChanged();
        }
    }
    public string? Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ClienteViewModel()
    {
        Cliente = new ObservableCollection<ClienteModel>();
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
