using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SyncfusionXamarinApp1
{
    public partial class MainPage : ContentPage
    {
        ViewModel.MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModel.MainPageViewModel(this);
            viewModel.filterTextChanged = OnFilterChanged;
        }

        private void OnFilterChanged()
        {
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = viewModel.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                viewModel.FilterText = "";
            else
                viewModel.FilterText = e.NewTextValue;
        }
    }
}
