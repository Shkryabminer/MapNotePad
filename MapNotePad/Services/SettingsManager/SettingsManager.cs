﻿using MapNotePad;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MapNotePad.Services
{
    public class SettingsManager : ISettingsManager
    {

        private readonly ISettings _setPlugin;

        public SettingsManager(ISettings plugin)
        {
            _setPlugin = plugin;
        }

        #region --ISettings implementation--
       
        public string AutorizatedUserEmail
        {
            get => _setPlugin.GetValueOrDefault(nameof(AutorizatedUserEmail), string.Empty);
            set => _setPlugin.AddOrUpdateValue(nameof(AutorizatedUserEmail), value);
        }
        public int SelectedSortMethode
        {
            get => _setPlugin.GetValueOrDefault(nameof(SelectedSortMethode), 0);
            set => _setPlugin.AddOrUpdateValue(nameof(SelectedSortMethode), value);
        }
        public string LanguageSource
        {
            get => _setPlugin.GetValueOrDefault(nameof(LanguageSource), Constants._english);
            set => _setPlugin.AddOrUpdateValue(nameof(LanguageSource), value);
        }

        public double CameraLatitude
        {
            get => _setPlugin.GetValueOrDefault(nameof(CameraLatitude), 41.0);
            set => _setPlugin.AddOrUpdateValue(nameof(CameraLatitude), value);
        }

        public double CameraLongitude
        {
            get => _setPlugin.GetValueOrDefault(nameof(CameraLongitude), 41.0);
            set => _setPlugin.AddOrUpdateValue(nameof(CameraLongitude), value);
        }

        public double Zoom
        {
            get => _setPlugin.GetValueOrDefault(nameof(Zoom), 41.0);
            set => _setPlugin.AddOrUpdateValue(nameof(Zoom), value);
        }
        public string FirstName
        {
            get => _setPlugin.GetValueOrDefault(nameof(FirstName),string.Empty);
            set => _setPlugin.AddOrUpdateValue(nameof(FirstName),value);
        }
        public string LastName
        {
            get => _setPlugin.GetValueOrDefault(nameof(LastName), string.Empty);
            set => _setPlugin.AddOrUpdateValue(nameof(LastName), value);
        }

        #endregion



    }
}
