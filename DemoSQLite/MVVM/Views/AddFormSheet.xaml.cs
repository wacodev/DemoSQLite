using DemoSQLite.MVVM.ViewModels;
using The49.Maui.BottomSheet;

namespace DemoSQLite.MVVM.Views;

public partial class AddFormSheet : BottomSheet
{
    public event Action RefreshList;

    public AddFormSheet()
	{
		InitializeComponent();

        AddFormViewModel viewModel = new();
        BindingContext = viewModel;

		viewModel.CloseSheet += CloseSheet;
	}

    private async void CloseSheet()
    {
        await DismissAsync();
        RefreshList?.Invoke();
    }
}