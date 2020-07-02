using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Calculator.Token
{
    public class CalculatorEntry : INotifyPropertyChanged
    {
        private const string INITIAL_ENTRY = "0";

        private string _entry = INITIAL_ENTRY;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Entry
        {
            get
            {
                if (_entry.Contains(".") && _entry.Length == (_entry.IndexOf('.') + 1))
                {
                    return $"{_entry}0";
                }

                return _entry;
            }

            set
            {
                if(_entry != value)
                {
                    _entry = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsValid { get; private set; }

        public void RemoveEntry() => Entry = Entry.Length > 1 ? _entry.Remove(_entry.Length - 1) : INITIAL_ENTRY;
        public void SetFloatingEntry() => AddEntry(".");
        public bool IsInitialEntry() => Entry.Equals(INITIAL_ENTRY);

        public void ClearEntry()
        {
            Entry = INITIAL_ENTRY;
            IsValid = false;
        }
        public void AddEntry(string entry)
        {
            Entry = IsInitialEntry() ? entry : Entry + entry;
            IsValid = true;
        }

        public void NegateEntry()
        {
            if (IsInitialEntry() == false && double.TryParse(Entry, out double entryValue) == true)
            {
                Entry = (-entryValue).ToString();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
