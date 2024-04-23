using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using DemoSQLite.MVVM.Models;
using DemoSQLite.MVVM.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoSQLite.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MenuViewModel
    {
        public MyTask Task { get; set; }
        public string ActionDescription { get; set; }
        public ICommand CheckTaskCommand => new Command<MyTask>(CheckTask);
        public ICommand EditTaskCommand => new Command<MyTask>(ShowEditFormTask);
        public ICommand DeleteTaskCommand => new Command<MyTask>(DeleteTask);
        readonly CancellationTokenSource cancellationTokenSource = new();
        public event Action<bool> CloseSheet;

        public MenuViewModel(MyTask task)
        {
            Task = task;

            if (task.IsChecked)
            {
                ActionDescription = "Desmarcar tarea";
            }
            else
            {
                ActionDescription = "Marcar tarea";
            }
        }

        private async void CheckTask(MyTask task)
        {
            string message = string.Empty;

            try
            {
                task.IsChecked = !task.IsChecked;
                App.TaskRepository.SaveItem(task);

                message = task.IsChecked
                    ? "Tarea marcada con éxito"
                    : "Tarea desmarcada con éxito";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                message = "Ocurrió un error";
            }

            var toast = Toast.Make(message, ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);

            CloseSheet?.Invoke(true);
        }

        private async void ShowEditFormTask(MyTask task)
        {
            EditFormSheet sheet = new(task);
            sheet.RefreshList += RefreshList;
            await sheet.ShowAsync();
        }

        private async void DeleteTask(MyTask task)
        {
            string message = string.Empty;

            try
            {
                App.TaskRepository.DeleteItem(task);
                message = "Tarea eliminada con éxito";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                message = "Ocurrió un error";
            }

            var toast = Toast.Make(message, ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);

            CloseSheet?.Invoke(true);
        }

        private void RefreshList()
        {
            CloseSheet?.Invoke(true);
        }
    }
}
