using BT2.UI.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BT2.UI.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private ObservableCollection<Suplier> _list;
        public ObservableCollection<Suplier> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        private DateTime? _contractDate;
        public DateTime? ContractDate { get => _contractDate; set { _contractDate = value; OnPropertyChanged(); } }

        private Suplier _selectedItem;
        public Suplier SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.QuanLyKhoContext.Supliers);
            AddCommand = new RelayCommand<object>((p) =>
            {                
                return true;
            }, (p) =>
            {
                var suplier = new Suplier { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate };
                DataProvider.Ins.QuanLyKhoContext.Supliers.Add(suplier);
                DataProvider.Ins.QuanLyKhoContext.SaveChanges();

                List.Add(suplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.QuanLyKhoContext.Supliers.Where(u => u.Id.Equals(SelectedItem.Id));

                if (displayList != null && displayList.Count() != 0) return true;

                return false;
            }, (p) =>
            {
                var suplier = DataProvider.Ins.QuanLyKhoContext.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                suplier.DisplayName = DisplayName;
                suplier.Address = Address; 
                suplier.Phone = Phone; 
                suplier.Email = Email; 
                suplier.MoreInfo = MoreInfo; 
                suplier.ContractDate = ContractDate;

                DataProvider.Ins.QuanLyKhoContext.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                SelectedItem.Address = Address;
                SelectedItem.Phone = Phone;
                SelectedItem.Email = Email;
                SelectedItem.MoreInfo = MoreInfo;
                SelectedItem.ContractDate = ContractDate;
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new Suplier() { Id = SelectedItem.Id, 
                                                  DisplayName = SelectedItem.DisplayName, 
                                                  Address = SelectedItem.Address, 
                                                  Phone = SelectedItem.Phone,
                                                  Email = SelectedItem.Email,
                                                  MoreInfo = SelectedItem.MoreInfo,
                                                  ContractDate = SelectedItem.ContractDate };
                        break;
                    }
                }
            });
        }
    }
}
