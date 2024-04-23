using System.Windows.Input;
using DemoSQLite.MVVM.Models;
using PropertyChanged;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;

namespace DemoSQLite.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditFormViewModel
    {
        public MyTask Task { get; set; }
        public ICommand EditTaskCommand => new Command<MyTask>(EditTask);
        readonly CancellationTokenSource cancellationTokenSource = new();
        public event Action CloseSheet;

        public EditFormViewModel(MyTask task)
        {
            Task = task;
        }

        private async void EditTask(MyTask task)
        {
            string message = string.Empty;

            try
            {
                App.TaskRepository.SaveItem(task);
                message = "Tarea editada con éxito";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                message = "Ocurrió un error";
            }

            var toast = Toast.Make(message, ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);

            CloseSheet?.Invoke();
        }
    }
}
