using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFLearn.Infrastructure.Commands;
using WPFLearn.Models;
using WPFLearn.ViewModels.Base;

namespace WPFLearn.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /*-------------------------------------------------------------------*/

        public ObservableCollection<ProvisionOfServices> ProvisionOfServices { get; }

        #region SelectedProvisionOfServices

        /// <summary>Выбранный договор</summary>
        private ProvisionOfServices _ProvisionOfServices;

        /// <summary>Выбранный договор</summary>
        public ProvisionOfServices pOfServices 
        { 
            get => _ProvisionOfServices;
            set => Set(ref _ProvisionOfServices, value);
        }

        #endregion

        #region Заголовок главного окна

        private string _Title = "Главное окно";
        /// <summary> Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        /*-------------------------------------------------------------------*/

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #endregion

        /*-------------------------------------------------------------------*/

        public MainWindowViewModel() 
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);

            #endregion

            var services = Enumerable.Range(1, 10).Select(i => new Service
            {
                Name = $"Услуга {i}"
            });


            var provision = Enumerable.Range(1, 20).Select(i => new ProvisionOfServices
            {
                Name = $"Договор №{i}",
                Date = DateTime.Now,
                Services = new ObservableCollection<Service>(services),
                Clients = new Clients($"Клиент {i}")
            });

            ProvisionOfServices = new ObservableCollection<ProvisionOfServices>(provision);

        }

        /*-------------------------------------------------------------------*/
    }
}
