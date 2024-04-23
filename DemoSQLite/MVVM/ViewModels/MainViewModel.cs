using DemoSQLite.MVVM.Models;
using DemoSQLite.MVVM.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DemoSQLite.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<MyTask> TaskList { get; set; } = [];
        public ICommand ShowAddFormCommand => new Command(ShowAddForm);
        public ICommand ShowMenuCommand => new Command<MyTask>(ShowMenu);

        public MainViewModel()
        {
            FillList();
        }

        private void FillList()
        {
            try
            {
                TaskList.Clear();

                var response = App.TaskRepository.GetItems();

                foreach (var item in response)
                {
                    item.TextDecoration = item.IsChecked
                        ? TextDecorations.Strikethrough
                        : TextDecorations.None;

                    TaskList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        private async void ShowAddForm()
        {
            AddFormSheet sheet = new();
            sheet.RefreshList += FillList;
            await sheet.ShowAsync();
        }

        private async void ShowMenu(MyTask task)
        {
            MenuSheet sheet = new(task);
            sheet.RefreshList += FillList;
            await sheet.ShowAsync();
        }
    }
}
