using System.Windows;
using System.Windows.Input;
using WPFLearn.Infrastructure.Commands;
using WPFLearn.ViewModels.Base;

namespace WPFLearn.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Заголовок главного окна
        
        private string _Title = "Главное окно";
        /// <summary> Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

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

        public MainWindowViewModel() 
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);

            #endregion
        }
    }
}
