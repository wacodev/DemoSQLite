using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using DemoSQLite.MVVM.Models;
using DemoSQLite.MVVM.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoSQLite.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AddFormViewModel
    {
        public string Description { get; set; }
        public ICommand AddTaskCommand => new Command(AddTask);
        readonly CancellationTokenSource cancellationTokenSource = new();
        public event Action CloseSheet;

        private async void AddTask()
        {
            string message = string.Empty;

            try
            {
                MyTask task = new MyTask()
                {
                    Description = Description,
                    IsChecked = false,
                    TextDecoration = TextDecorations.None
                };
                App.TaskRepository.SaveItem(task);

                message = "Tarea agregada con éxito";
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
