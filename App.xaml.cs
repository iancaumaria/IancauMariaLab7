using System;

using System.IO;
using IancauMariaLab7.Data;

namespace IancauMariaLab7
{
    public partial class App : Application
    {
        static ShoppingListDatabase database;
        public static ShoppingListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}