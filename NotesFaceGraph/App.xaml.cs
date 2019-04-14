using System;
using System.IO;
using NotesFaceGraph.Views;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NotesFaceGraph
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer)
        {
            InitializeComponent();

        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(RegisterDatabase());
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<CreateNewUserPage>();
            containerRegistry.RegisterForNavigation<NotesListPage>();
            containerRegistry.RegisterForNavigation<NoteDetailsPage>();
        }

        private SQLiteAsyncConnection RegisterDatabase()
        {
            string dbName = "notes.db3";
            string databaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
            var con = new SQLiteAsyncConnection(databaseFolder);
            return con;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
