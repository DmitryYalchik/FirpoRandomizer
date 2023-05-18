using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirpoRandomizer.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace FirpoRandomizer.ViewModels
{
    class AddWindowViewModel : BindableBase
    {
        private MainViewModel _mainViewModel;
        public AddWindowViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        private DelegateCommand<Window> _closeButton;
        public DelegateCommand<Window> CloseButton =>
            _closeButton ?? (_closeButton = new DelegateCommand<Window>(ExecuteCloseButton));

        void ExecuteCloseButton(Window parameter)
        {
            if (parameter is AddWindow)
            {
                parameter.Close();
            }
        }


        private string currentNum = "";
        public string CurrentNum
        {
            get { return currentNum; }
            set { SetProperty(ref currentNum, value); }
        }


        private DelegateCommand _addButton;
        public DelegateCommand AddButton =>
            _addButton ?? (_addButton = new DelegateCommand(ExecuteAddButton, CanExecuteAddButton).ObservesProperty(()=>CurrentNum));

        void ExecuteAddButton()
        {
            _mainViewModel.Slots.Add(new Models.Slot() { Number = CurrentNum });
            _mainViewModel.CountFreeSlots = _mainViewModel.Slots.Count();
            CurrentNum = "";
        }

        bool CanExecuteAddButton()
        {
            if (CurrentNum != "")
            {
                bool aboba = _mainViewModel.Slots.Any(x => x.Number == CurrentNum);
                if ( aboba)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }



        private DelegateCommand _clearButton;
        public DelegateCommand ClearButton =>
            _clearButton ?? (_clearButton = new DelegateCommand(ExecuteClearButton, CanExecuteClearButton).ObservesProperty(()=>CurrentNum));

        void ExecuteClearButton()
        {
            CurrentNum = "";
        }

        bool CanExecuteClearButton()
        {
            return String.IsNullOrEmpty(CurrentNum) == false ? true : false;
        }



        private DelegateCommand<string> _numberButton;
        public DelegateCommand<string> NumberButton =>
            _numberButton ?? (_numberButton = new DelegateCommand<string>(ExecuteNumberButton, CanExecuteNumberButton).ObservesProperty(() => CurrentNum));

        void ExecuteNumberButton(string parameter)
        {
            if (parameter != null)
            {
                CurrentNum += parameter;
            }
        }

        bool CanExecuteNumberButton(string parameter)
        {
            return CurrentNum.Length <= 3 == true ? true : false;
        }
    }
}
