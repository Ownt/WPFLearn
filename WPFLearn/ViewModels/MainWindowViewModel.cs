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

        #region CreateProvisionOfServicesCommand

        /// <summary> Команда добавления договора </summary>
        public ICommand CreateNewProvisionOfServicesCommand { get; }

        private bool CanCreateNewProvisionOfServicesExecute(object p) => true;

        private void OnCreateNewProvisionOfServicesExecute(object p)
        {
            var provisionOfService_max_index = ProvisionOfServices.Count + 1;
            var new_provisionOfService = new ProvisionOfServices
            {
                Name = $"Договор №{provisionOfService_max_index}",
                Date = DateTime.Now,
                Services = new ObservableCollection<Service>(),
                Clients = new Clients { Name = $"Клинет {provisionOfService_max_index}" }
            };

            ProvisionOfServices.Add(new_provisionOfService);
        }

        #endregion

        #region DeleteProvisionOfServicesCommand

        /// <summary> Команда удаления договора </summary>
        public ICommand DeleteProvisionOfServicesCommand { get; }

        private bool CanDeleteProvisionOfServicesExecute(object p) => p is ProvisionOfServices && ProvisionOfServices.Contains(pOfServices);

        private void OnDeleteProvisionOfServicesExecute(object p)
        {
            if (!(p is ProvisionOfServices)) return;
            var index = ProvisionOfServices.IndexOf(pOfServices);
            ProvisionOfServices.Remove(pOfServices);
            if (index < ProvisionOfServices.Count)
                pOfServices = ProvisionOfServices[index];
        }

        #endregion

        #endregion

        /*-------------------------------------------------------------------*/

        public MainWindowViewModel() 
        {
            #region Команды

            /// <summary> Закрытие приложения </summary>
            CloseApplicationCommand = new LambdaCommand(
                OnCloseApplicationCommandExecuted, 
                CanCloseApplicationCommandExecuted);

            /// <summary> Добавление договора </summary>
            CreateNewProvisionOfServicesCommand = new LambdaCommand(
                OnCreateNewProvisionOfServicesExecute, 
                CanCreateNewProvisionOfServicesExecute);

            /// <summary> Удаление договора </summary>
            DeleteProvisionOfServicesCommand = new LambdaCommand(
                OnDeleteProvisionOfServicesExecute, 
                CanDeleteProvisionOfServicesExecute);


            #endregion

            var services = Enumerable.Range(1, 10).Select(i => new Service
            {
                Name = $"Услуга {i}",
                Price = 100 * i,
                Description = $"Очень длинной описание предоставляемой услуги {i}"
            });

            var provision = Enumerable.Range(1, 20).Select(i => new ProvisionOfServices
            {
                Name = $"Договор №{i}",
                Date = DateTime.Now,
                Services = new ObservableCollection<Service>(services),
                Clients = new Clients
                {
                    Name = $"Клиент {i}",
                    Number = $"{i}{i * 2}{i * 7 / 2}",
                    Description = "Описание клиента"
                }
            });

            ProvisionOfServices = new ObservableCollection<ProvisionOfServices>(provision);

        }

        /*-------------------------------------------------------------------*/
    }
}
