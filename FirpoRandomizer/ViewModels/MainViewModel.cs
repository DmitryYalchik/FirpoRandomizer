using System;
using System.Collections.ObjectModel;
using System.Linq;
using FirpoRandomizer.Models;
using FirpoRandomizer.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace FirpoRandomizer.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AddWindow addWindow;
        public MainViewModel()
        {

        }


        private int countFreeSlots;
        public int CountFreeSlots
        {
            get { return countFreeSlots; }
            set { SetProperty(ref countFreeSlots, value); }
        }


        private string chooseSlot = "";
        public string ChooseSlot
        {
            get { return chooseSlot; }
            set { SetProperty(ref chooseSlot, value); }
        }


        private DelegateCommand<Slot> _deleteSlot;
        public DelegateCommand<Slot> DeleteSlot =>
            _deleteSlot ?? (_deleteSlot = new DelegateCommand<Slot>(ExecuteDeleteSlot));

        void ExecuteDeleteSlot(Slot parameter)
        {
            Slots.Remove(parameter);
        }


        private DelegateCommand _takeFreeSlot;
        public DelegateCommand TakeFreeSlot =>
            _takeFreeSlot ?? (_takeFreeSlot = new DelegateCommand(ExecuteTakeFreeSlot, CanExecuteTakeFreeSlot).ObservesProperty(()=>CountFreeSlots));

        void ExecuteTakeFreeSlot()
        {
            string[] allFreeSlots = Slots.Where(x => x.IsFree == true).Select(x=>x.Number).ToArray();
            if (allFreeSlots.Length != 0)
            {
                int rand = new Random().Next(0, allFreeSlots.Length - 1);
                var a = Slots.Where(x=>x.IsFree == true).FirstOrDefault(x => x.Number == allFreeSlots[rand]);
                if (a != null)
                {
                    Slots.FirstOrDefault(x => x.Number == allFreeSlots[rand]).IsFree = false;
                    ChooseSlot = Slots.FirstOrDefault(x => x.Number == allFreeSlots[rand]).Number;
                    CountFreeSlots = allFreeSlots.Length - 1;
                }
            }
        }

        bool CanExecuteTakeFreeSlot()
        {
            return Slots.Where(x => x.IsFree == true).Count() == 0 ? false : true;
        }



        private ObservableCollection<Slot> slots = new ObservableCollection<Slot>();
        public ObservableCollection<Slot> Slots
        {
            get { return slots; }
            set { CountFreeSlots = Slots.Count(); SetProperty(ref slots, value); }
        }



        private DelegateCommand _openAddWindow;
        public DelegateCommand OpenAddWindow =>
            _openAddWindow ?? (_openAddWindow = new DelegateCommand(ExecuteOpenAddWindow));

        void ExecuteOpenAddWindow()
        {
            addWindow = new AddWindow(this);
            addWindow.ShowDialog();
        }
    }
}
