using DemoSQLite.MVVM.Models;
using DemoSQLite.MVVM.ViewModels;
using MauiIcons.Core;
using The49.Maui.BottomSheet;

namespace DemoSQLite.MVVM.Views;

public partial class MenuSheet : BottomSheet
{
    public event Action RefreshList;

    public MenuSheet(MyTask task)
	{
		InitializeComponent();

        _ = new MauiIcon();

        MenuViewModel viewModel = new(task);
        BindingContext = viewModel;

        viewModel.CloseSheet += CloseSheet;
    }

    private async void CloseSheet(bool refresh)
    {
        await DismissAsync();

        if (refresh)
        {
            RefreshList?.Invoke();
        }
    }
}