using ModernUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModernUI.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;
using System.Runtime.CompilerServices;

namespace ModernUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private IUserReposity userReposity;

        //Properties
        public string Username
        {
            get => _username; set {
                _username = value;
                OnpropertyChanged(nameof(Username));
            } }
        public SecureString Password { get => _password; set
            {
                _password = value;
                OnpropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage { get => _errorMessage; set
            {
                _errorMessage = value;
                OnpropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get => _isViewVisible; set
            {
                _isViewVisible = value;
                OnpropertyChanged(nameof(IsViewVisible));
            }
        }
        //Commands
        public ICommand LoginCommand { get; } //get lenh login
        public ICommand ReCoverPasswordCommand { get; } //get lenh RecoverPassword
        public ICommand ShowpasswordCommand { get; } // get lenh Showpassword
        public ICommand RememberPasswordCommand { get; } // get lenh RememberPassword

        //Constructor
        public LoginViewModel()
        {
            userReposity = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            ReCoverPasswordCommand = new ViewModelCommand(ExecuteRecoverPasswordCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userReposity.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Invalid Username or Password !";
            }
        }
        private void ExecuteRecoverPasswordCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
