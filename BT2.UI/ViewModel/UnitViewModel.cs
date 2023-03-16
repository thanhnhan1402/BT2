using BT2.UI.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BT2.UI.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private ObservableCollection<Unit> _list;
        public ObservableCollection<Unit> List { get => _list; set { _list = value;OnPropertyChanged(); } }
        private Unit _selectedItem;
        public Unit SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); if (SelectedItem != null) { DisplayName = SelectedItem.DisplayName; } } }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Ins.QuanLyKhoContext.Units);
            AddCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.Ins.QuanLyKhoContext.Units.Where(u => u.DisplayName.Equals(DisplayName));

                if (displayList == null || displayList.Count() != 0) return false;

                return true;
            }, (p) => {
                var unit = new Unit { DisplayName = DisplayName };
                DataProvider.Ins.QuanLyKhoContext.Units.Add(unit);
                DataProvider.Ins.QuanLyKhoContext.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.QuanLyKhoContext.Units.Where(u => u.DisplayName.Equals(DisplayName));

                if (displayList == null || displayList.Count() != 0) return false;

                return true;
            }, (p) => {
                var unit = DataProvider.Ins.QuanLyKhoContext.Units.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                unit.DisplayName = DisplayName;
                DataProvider.Ins.QuanLyKhoContext.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new Unit() { Id = SelectedItem.Id, DisplayName = SelectedItem.DisplayName };
                        break;
                    }
                }
            });
        }
    }
}
