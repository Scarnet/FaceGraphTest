using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using NotesFaceGraph.Enums;
using NotesFaceGraph.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using Xamarin.Forms;

namespace NotesFaceGraph.ViewModels
{
    public class NoteDetailsPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Statuses { get => Enum.GetNames(typeof(Status)).ToList(); }


        public ICommand SaveCommand { get; set; }
        public ICommand TakeImageCommand { get; set; }

        private INavigationService _navigationService;
        private Note _note;
        private SQLiteAsyncConnection _con;
        private bool _isEdit;
        public NoteDetailsPageViewModel(SQLiteAsyncConnection con, INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new Command(HandSave);
            TakeImageCommand = new Command(HandleTakeImage);
            _con = con;
            SelectedStatus = Status.Open.ToString();
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _isEdit = parameters.GetValue<bool>("IsEdit");
            if (!_isEdit)
                return;

            _note = parameters.GetValue<Note>("Note");
            Title = _note.Title;
            Description = _note.Description;
            Image = _note.Image;
            SelectedStatus = _note.Status.ToString();
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        private async void HandleTakeImage()
        {
            var media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
            byte[] buffer = File.ReadAllBytes(media.Path);
            Image = media.Path;
        }

        private void HandSave()
        {
            if (_note == null)
                _note = new Note() { Date = DateTime.Now, UserId = Convert.ToInt32(Application.Current.Properties["UserId"]) };


            _note.Title = _title;
            _note.Description = _description;
            _note.Image = _image;
            _note.Status = (Status)Enum.Parse(typeof(Status), _selectedStatus);

            if (!_isEdit)
                _con.InsertAsync(_note);
            else
                _con.InsertOrReplaceAsync(_note);

            _navigationService.GoBackAsync();
        }

    }
}
