using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Services
{
    public class PackagesService
    {
        private List<string> _names;
        public List<string> Names
        {
            get => _names;
            set
            {
                if (_names != value)
                {
                    _names = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action OnChange;

        public PackagesService()
        {
            _names = new List<string>();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        // Add methods to manipulate the list
        public void AddName(string name)
        {
            if (!_names.Contains(name))
            {
                _names.Add(name);
                NotifyStateChanged();
            }
        }

        public void RemoveName(string name)
        {
            if (_names.Remove(name))
            {
                NotifyStateChanged();
            }
        }

        public void ClearNames()
        {
            _names.Clear();
            NotifyStateChanged();
        }
    }
}
