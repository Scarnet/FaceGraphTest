using System;
using System.Linq;
using System.Windows.Input;
using NotesFaceGraph.Models;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using Xamarin.Forms;

namespace NotesFaceGraph.ViewModels
{
    public class CreateNewUserPageViewModel : BindableBase
    {
        private string _usernName;
        public string UserName
        {
            get => _usernName;
            set
            {
                _usernName = value;
                RaisePropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CreateNewUserCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private INavigationService _navigation;
        private SQLiteAsyncConnection _con;
        public CreateNewUserPageViewModel(SQLiteAsyncConnection con, INavigationService navigation)
        {
            _navigation = navigation;
            _con = con;

            CreateNewUserCommand = new Command(HandleCreateNewUser);
            CancelCommand = new Command(HandleCancel);
        }


        private async void HandleCreateNewUser()
        {
            var info = await _con.GetTableInfoAsync("User");
            if (!info.Any())
                await _con.CreateTableAsync<User>();

            var user = new User
            {
                UserName = _usernName,
                Password = _password
            };

            await _con.InsertAsync(user);
            await _navigation.GoBackAsync();
        }

        private void HandleCancel()
        {
            _navigation.GoBackAsync();
        }

    }
}
