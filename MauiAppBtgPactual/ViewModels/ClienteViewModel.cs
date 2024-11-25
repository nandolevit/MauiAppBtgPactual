using MauiAppBtgPactual.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MauiAppBtgPactual.ViewModels;

public sealed class ClienteViewModel : BaseViewModel
{
    public ObservableCollection<ClienteModel>? ClienteObservable { get; set; }
    public ICommand CancelCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    private Guid _id;
    private string? _name;
    private string? _lastName;
    private string? _age;
    private string? _address;

    public bool IsDelete
    {
        get => !ID.Equals(Guid.Empty);
    }

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
            _lastName = Regex.Match(value, @"[a-zA-Z\s]+").Value;
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


    public ClienteViewModel()
    {
        this.PropertyChanged += CanRefresh!;
        ClienteObservable = new ObservableCollection<ClienteModel>();
        CancelCommand = new Command(execute: async () =>
        {
            var valid = !(string.IsNullOrEmpty(Name) &&
            string.IsNullOrEmpty(LastName) &&
            string.IsNullOrEmpty(Age) &&
            string.IsNullOrEmpty(Address));

            if (valid)
            {
                string msg = "Deseja descartar ";
                if (ID.Equals(Guid.Empty))
                    msg += "os dados?";
                else
                    msg += "as alterações?";

                if (!(await App.Current!.MainPage!
                .DisplayAlert("Cancelar", msg,
                "Sim", "Não")))
                {
                    return;
                }
            }

            await ((NavigationPage)App.Current!.MainPage!).PopAsync();
            await LimparCampos();
        });

        SaveCommand = new Command(execute: async () =>
        {
            if (ID.Equals(Guid.Empty))
            {
                ID = Guid.NewGuid();
            }
            else
            {
                var cliente = ClienteObservable.First(f => f.ID == ID);
                ClienteObservable.Remove(cliente);
            }

            ClienteObservable.Add(new ClienteModel
            {
                ID = ID,
                Address = Address,
                Age = Convert.ToInt32(Age),
                LastName = LastName,
                Name = Name
            });

            await LimparCampos("Registro salvo com sucesso!");
        },
        canExecute: () =>
        {
            var valid = !(string.IsNullOrEmpty(Name) ||
            string.IsNullOrEmpty(LastName) ||
            string.IsNullOrEmpty(Age) ||
            string.IsNullOrEmpty(Address));

            return valid;
        });

        DeleteCommand = new Command(execute: async () =>
        {
            var cliente = ClienteObservable.First(f => f.ID == ID);
            if (await App.Current!.MainPage!
            .DisplayAlert("Deletar", $"Deseja deletar o registro de {cliente.Name}?",
            "Sim", "Não"))
            {
                ClienteObservable.Remove(cliente);

                await LimparCampos("Registro deletado com sucesso!");
            }

        },
        canExecute:() =>
        {
            return !ID.Equals(Guid.Empty);
        });
    }

    private async Task LimparCampos(string? txt = null)
    {
        ID = Guid.Empty;
        Name = string.Empty;
        LastName = string.Empty;
        Age = string.Empty;
        Address = string.Empty;

        if (txt is not null)
            await App.Current!.MainPage!.DisplayAlert("Comando", txt, "OK");

        await ((NavigationPage)App.Current!.MainPage!).PopAsync();
    }

    private void CanRefresh(object sender, PropertyChangedEventArgs args)
    {
        (SaveCommand as Command)!.ChangeCanExecute();
    }
}
