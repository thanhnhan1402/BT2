using BT2.UI.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BT2.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Stock> _stockList;
        public ObservableCollection<Stock> StockList { get => _stockList; set { _stockList = value; OnPropertyChanged(); } }
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                    return;

                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;

                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLoggedIn)
                {
                    p.Show();
                    LoadStockList();
                }
                else
                {
                    p.Close();
                }
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });

            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });

            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });

            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });

            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });

            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });
        }

        private void LoadStockList()
        {
            StockList = new ObservableCollection<Stock>();
            var objectList = DataProvider.Ins.QuanLyKhoContext.Objects;
            int i = 1;
            foreach (var item in objectList)
            {
                var inputList = DataProvider.Ins.QuanLyKhoContext.InputInfos.Where(i => i.IdObject == item.Id);
                var outputList = DataProvider.Ins.QuanLyKhoContext.OutputInfos.Where(i => i.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() > 0)
                {
                    sumInput = (int)inputList.Sum(i => i.Quantity);
                }

                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(i => i.Quantity);
                }

                Stock stock = new Stock();
                stock.STT = i;
                stock.InStock = sumInput - sumOutput;
                stock.Object = item;

                StockList.Add(stock);

                i++;
            }
        }
    }
}
