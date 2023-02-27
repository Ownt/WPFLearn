using System;
using System.Collections.Generic;
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

        public ObservableCollection<Contracts> Contracts { get; }

        #region SelectedContract

        /// <summary>Выбранный договор</summary>
        private Contracts _SelectedContract;

        /// <summary>Выбранный договор</summary>
        public Contracts selectedContract 
        { 
            get => _SelectedContract;
            set => Set(ref _SelectedContract, value);
        }

        #endregion

        #region Заголовок главного окна

        private string _Title = "Главное окно";
        /// <summary> Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        public IEnumerable<Clients> TestClients =>
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(i => new Clients
            {
                Name = $"Клиент {i}",
                Number = $"Номер клиента {i}"
            });

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

        #region CreateNewContractCommand

        /// <summary> Команда добавления договора </summary>
        public ICommand CreateNewContractCommand { get; }

        private bool CanCreateNewContractCommandExecute(object p) => true;

        private void OnCreateNewContractCommandExecute(object p)
        {
            var provisionOfService_max_index = Contracts.Count + 1;
            var new_provisionOfService = new Contracts
            {
                Name = $"Договор №{provisionOfService_max_index}",
                Date = DateTime.Now,
                Services = new ObservableCollection<Service>(),
                Clients = new Clients { Name = $"Клинет {provisionOfService_max_index}" }
            };

            Contracts.Add(new_provisionOfService);
        }

        #endregion

        #region DeleteContractCommand

        /// <summary> Команда удаления договора </summary>
        public ICommand DeleteContractCommand { get; }

        private bool CanDeleteContractCommandExecute(object p) => p is Contracts && Contracts.Contains(selectedContract);

        private void OnDeleteContractCommandExecute(object p)
        {
            if (!(p is Contracts)) return;
            var index = Contracts.IndexOf(selectedContract);
            Contracts.Remove(selectedContract);
            if (index < Contracts.Count)
                selectedContract = Contracts[index];
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
            CreateNewContractCommand = new LambdaCommand(
                OnCreateNewContractCommandExecute, 
                CanCreateNewContractCommandExecute);

            /// <summary> Удаление договора </summary>
            DeleteContractCommand = new LambdaCommand(
                OnDeleteContractCommandExecute, 
                CanDeleteContractCommandExecute);


            #endregion
            var services_index = 1;
            var services = Enumerable.Range(1, 10).Select(i => new Service
            {
                Name = $"Услуга {services_index}",
                Price = 100 * services_index,
                Description = $"Очень длинной описание предоставляемой услуги {services_index++}"
            });

            var contracts = Enumerable.Range(1, 20).Select(i => new Contracts
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

            Contracts = new ObservableCollection<Contracts>(contracts);

        }

        /*-------------------------------------------------------------------*/
    }
}
