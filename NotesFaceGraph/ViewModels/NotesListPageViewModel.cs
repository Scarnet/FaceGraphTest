using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using NotesFaceGraph.Enums;
using NotesFaceGraph.Models;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using Xamarin.Forms;

namespace NotesFaceGraph.ViewModels
{
    public class NotesListPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                RaisePropertyChanged();
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                HandleItemSelectedChanged();
            }
        }


        public ICommand CreateNewCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        private INavigationService _navigation;
        private SQLiteAsyncConnection _con;

        public NotesListPageViewModel(SQLiteAsyncConnection con, INavigationService navigation)
        {
            _navigation = navigation;
            _con = con;
            CreateNewCommand = new Command(HandleCreateNew);
            LogoutCommand = new Command(HandleLogout);
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var info = await _con.GetTableInfoAsync("Note");
            var userId = Convert.ToInt32(Application.Current.Properties["UserId"]);
            if (!info.Any())
                await _con.CreateTableAsync<Note>();

            var hh = await _con.Table<Note>().ToListAsync();
            var notes = await _con.Table<Note>().Where(note => note.UserId == userId).ToListAsync();
            Notes = new ObservableCollection<Note>(notes);
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        private async void HandleCreateNew()
        {
            var parameters = new NavigationParameters
            {
                {"IsEdit", false}
            };

            var result = await _navigation.NavigateAsync("NoteDetailsPage", parameters);
        }


        private void HandleItemSelectedChanged()
        {
            var parameters = new NavigationParameters
            {
                { "IsEdit", true },
                { "Note", _selectedNote }
            };

            _navigation.NavigateAsync("NoteDetailsPage", parameters);
        }

        private void HandleLogout()
        {
            _navigation.NavigateAsync( new Uri("http://www.website.com/LoginPage", UriKind.Absolute));
            Application.Current.Properties["UserId"] = null;
        }

    }
}
