using DemoSQLite.Abstractions;
using DemoSQLite.MVVM.Models;
using Models = DemoSQLite.MVVM.Models;

namespace DemoSQLite
{
    public partial class App : Application
    {
        public static BaseRepository<MyTask> TaskRepository { get; private set; }

        public App(BaseRepository<MyTask> taskRepository)
        {
            InitializeComponent();

            TaskRepository = taskRepository;

            MainPage = new AppShell();
        }
    }
}
