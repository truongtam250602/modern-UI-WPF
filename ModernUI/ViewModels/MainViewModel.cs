﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using ModernUI.Model;
using ModernUI.Repositories;

namespace ModernUI.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserReposity userReposity;

        // properties
        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnpropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView 
        {
            get => _currentChildView;
            set 
            { 
                _currentChildView = value; 
                OnpropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        { 
            get => _caption; set
            {
                _caption = value;
                OnpropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon 
        { 
            get => _icon; set
            {
                _icon = value;
                OnpropertyChanged(nameof(Icon));
            }
        }

        // commandS
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public MainViewModel()
        {
            CurrentUserAccount = new UserAccountModel();
            userReposity = new UserRepository();

            //Initialize commands

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

            // Defaulf view 

            ExecuteShowHomeViewCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView=new CustomerViewModel();
            Caption = "Customer";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView =new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userReposity.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                {
                    CurrentUserAccount.UserName = user.Username;
                    CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";
                    CurrentUserAccount.ProfilePicture = null;
                };
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, no logged";
                // Hide child view .
            }
        }
    }
}
