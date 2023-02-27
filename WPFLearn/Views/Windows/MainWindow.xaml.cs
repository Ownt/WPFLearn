using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFLearn.Models;

namespace WPFLearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ContractsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Contracts contract)) return;
            if (contract.Name is null) return;
            
            var filter_text = ContractNameFilterText.Text;
            if (filter_text.Length == 0) return; 

            if (contract.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void OnContractFilterTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text_box = (TextBox) sender;
            var collection = (CollectionViewSource)text_box.FindResource("ContractsCollection");
            collection.View.Refresh();
        }
    }
}
