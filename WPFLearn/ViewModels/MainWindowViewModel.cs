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

    }
}
