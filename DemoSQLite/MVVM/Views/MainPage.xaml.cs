using DemoSQLite.MVVM.ViewModels;

namespace DemoSQLite.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }

}
