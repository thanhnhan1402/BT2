using BT2.UI.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Object = BT2.UI.Model.Object;

namespace BT2.UI.ViewModel
{
    public class ObjectViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private ObservableCollection<Object> _list;
        public ObservableCollection<Object> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private ObservableCollection<Unit> _unit;
        public ObservableCollection<Unit> Unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Suplier> _suplier;
        public ObservableCollection<Suplier> Suplier { get => _suplier; set { _suplier = value; OnPropertyChanged(); } }

        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private string _qrCode;
        public string QRCode { get => _qrCode; set { _qrCode = value; OnPropertyChanged(); } }

        private string _barCode;
        public string BarCode { get => _barCode; set { _barCode = value; OnPropertyChanged(); } }



        private Object _selectedItem;
        public Object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    QRCode = SelectedItem.Qrcode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.IdUnitNavigation;
                    SelectedSuplier = SelectedItem.IdSuplierNavigation;
                }
            }
        }

        private Unit _selectedUnit;
        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged();
            }
        }

        private Suplier _selectedSuplier;
        public Suplier SelectedSuplier
        {
            get => _selectedSuplier;
            set
            {
                _selectedSuplier = value;
                OnPropertyChanged();
            }
        }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Object>(DataProvider.Ins.QuanLyKhoContext.Objects);
            Unit = new ObservableCollection<Unit>(DataProvider.Ins.QuanLyKhoContext.Units);
            Suplier = new ObservableCollection<Suplier>(DataProvider.Ins.QuanLyKhoContext.Supliers);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSuplier == null || SelectedUnit == null)
                    return false;
                return true;
            }, (p) =>
            {
                var Object = new Object { Id = Guid.NewGuid().ToString(), DisplayName = DisplayName, IdUnit = SelectedUnit.Id, IdSuplier = SelectedSuplier.Id, Qrcode = QRCode, BarCode = BarCode };
                DataProvider.Ins.QuanLyKhoContext.Objects.Add(Object);
                DataProvider.Ins.QuanLyKhoContext.SaveChanges();

                List.Add(Object);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                    return false;

                var displayList = DataProvider.Ins.QuanLyKhoContext.Objects.Where(u => u.Id.Equals(SelectedItem.Id));

                if (displayList != null && displayList.Count() != 0) return true;

                return false;
            }, (p) =>
            {
                var Object = DataProvider.Ins.QuanLyKhoContext.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Object.DisplayName = DisplayName;
                Object.IdUnit = SelectedUnit.Id; 
                Object.IdSuplier = SelectedSuplier.Id; 
                Object.Qrcode = QRCode; 
                Object.BarCode = BarCode;
                Object.IdUnitNavigation = SelectedUnit;
                Object.IdSuplierNavigation = SelectedSuplier;

                DataProvider.Ins.QuanLyKhoContext.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                SelectedItem.IdUnit = SelectedUnit.Id;
                SelectedItem.IdSuplier = SelectedSuplier.Id;
                SelectedItem.Qrcode = QRCode;
                SelectedItem.BarCode = BarCode;
                SelectedItem.IdUnitNavigation = SelectedUnit;
                SelectedItem.IdSuplierNavigation = SelectedSuplier;
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new Object()
                        {
                            Id = SelectedItem.Id,
                            DisplayName = SelectedItem.DisplayName,
                            IdUnitNavigation = SelectedItem.IdUnitNavigation,
                            IdSuplierNavigation = SelectedItem.IdSuplierNavigation,
                            IdSuplier = SelectedItem.IdSuplier,
                            IdUnit = SelectedItem.IdUnit,
                            Qrcode = SelectedItem.Qrcode,
                            BarCode = SelectedItem.BarCode
                        };
                        break;
                    }
                }
            });
        }
    }
}
