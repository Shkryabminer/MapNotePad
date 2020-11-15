using MapNotePad.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MapNotePad.ViewModels
{
    public class PinModelViewModel : BindableBase
    {
        public PinModelViewModel(PinModel pinModel)
        {
            ID = pinModel.ID;
            Name = pinModel.Name;
            Latitude = pinModel.Latitude;
            Longtitude = pinModel.Longtitude;
            UserEmail = pinModel.UserEmail;
            KeyWords = pinModel.KeyWords;
            IsActive = pinModel.IsActive;
            Picture = pinModel.Picture;
        }
        public PinModelViewModel()
        { 
        }

        #region --Public properties--

        private int _id;
        public int ID
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longtitude;
        public double Longtitude
        {
            get => _longtitude;
            set => SetProperty(ref _longtitude, value);
        }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set => SetProperty(ref _userEmail, value);
        }

        private string _keyWords;
        public string KeyWords
        {
            get => _keyWords;
            set => SetProperty(ref _keyWords, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private string _picture;
        public string Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }
        #endregion

    }
}
