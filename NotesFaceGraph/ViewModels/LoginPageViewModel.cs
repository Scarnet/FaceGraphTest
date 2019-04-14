using System;
using System.Linq;
using System.Windows.Input;
using NotesFaceGraph.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SQLite;
using Xamarin.Forms;

namespace NotesFaceGraph.ViewModels
{
    public class LoginPageViewModel : BindableBase
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

        public ICommand LoginCommand { get; set; }
        public ICommand CreateNewUserCommand { get; set; }

        private INavigationService _navigation;
        private SQLiteAsyncConnection _con;
        private IPageDialogService _dialog;

        public LoginPageViewModel(INavigationService navigation, IPageDialogService dialog, SQLiteAsyncConnection con)
        {
            _navigation = navigation;
            _con = con;
            _dialog = dialog;
            LoginCommand = new Command(HandleLogin);
            CreateNewUserCommand = new Command(HandleCreateNewUser);
        }


        private async void HandleLogin()
        {
            var info = await _con.GetTableInfoAsync("User");
            if (!info.Any())
                await _con.CreateTableAsync<User>();

            var user = await _con.Table<User>().FirstOrDefaultAsync(u => u.UserName.ToLower() == _usernName.ToLower());

            if (user == null)
            {
                await _dialog.DisplayAlertAsync("Error", "Invalid credintials", "Ok");
                return;
            }

            if (_password != user.Password)
            {
                await _dialog.DisplayActionSheetAsync("Error", "Invalid password", "Ok");
                return;
            }

            if (!Application.Current.Properties.ContainsKey("UserId"))
                Application.Current.Properties.Add("UserId", user.Id);
            else
                Application.Current.Properties["UserId"] = user.Id;

            await _navigation.NavigateAsync(new Uri("/NavigationPage/NotesListPage"));
        }

        private void HandleCreateNewUser()
        {
            _navigation.NavigateAsync("CreateNewUserPage");
        }

    }
}
