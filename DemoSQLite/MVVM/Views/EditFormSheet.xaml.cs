using DemoSQLite.MVVM.Models;
using DemoSQLite.MVVM.ViewModels;
using The49.Maui.BottomSheet;

namespace DemoSQLite.MVVM.Views;

public partial class EditFormSheet : BottomSheet
{
    public event Action RefreshList;

    public EditFormSheet(MyTask task)
	{
		InitializeComponent();

        EditFormViewModel viewModel = new(task);
        BindingContext = viewModel;

        viewModel.CloseSheet += CloseSheet;
    }

    private async void CloseSheet()
    {
        await DismissAsync();
        RefreshList?.Invoke();
    }
}