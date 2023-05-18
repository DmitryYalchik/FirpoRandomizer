using Prism.Mvvm;

namespace FirpoRandomizer.Models
{
    public class Slot : BindableBase
    {
        private string number;
        public string Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }

        private bool isFree = true;
        public bool IsFree
        {
            get { return isFree; }
            set { SetProperty(ref isFree, value); }
        }

    }
}
