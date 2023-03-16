using BT2.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BT2.UI.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLoggedIn { get; set; }
        private string _userName;
        public string UserName { get => _userName; set { _userName = value;OnPropertyChanged(); } }
        private string _passWord;
        public string Password { get => _passWord; set { _passWord = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }  
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public LoginViewModel()
        {
            UserName = "";
            Password = "";
            IsLoggedIn = false;             
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

        }

        private void Login(Window p)
        {
            if (p == null)
                return;

            var accCount = DataProvider.Ins.QuanLyKhoContext.Users.Where(u => u.UserName == UserName && u.Password == Password).Count();

            if (accCount > 0)
            {
                IsLoggedIn = true;
                p.Close();
            } else
            {
                IsLoggedIn = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }                      
        }
    }
}
