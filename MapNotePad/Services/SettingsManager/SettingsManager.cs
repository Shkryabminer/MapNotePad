using MapNotePad;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MapNotePad.Services
{
    public class SettingsManager : ISettingsManager
    {

        private ISettings SetPlugin { get; set; }
        public SettingsManager(ISettings plugin)
        {
            SetPlugin = plugin;
        }

        #region --ISettings implementation--
        public int AutorizatedUserId
        {
            get => SetPlugin.GetValueOrDefault(nameof(AutorizatedUserId), -1);
            set => SetPlugin.AddOrUpdateValue(nameof(AutorizatedUserId), value);
        }
        public int SelectedSortMethode
        {
            get => SetPlugin.GetValueOrDefault(nameof(SelectedSortMethode), 0);
            set => SetPlugin.AddOrUpdateValue(nameof(SelectedSortMethode), value);
        }
        public string LanguageSource
        {
            get => SetPlugin.GetValueOrDefault(nameof(LanguageSource), Constants._english); 
            set => SetPlugin.AddOrUpdateValue(nameof(LanguageSource),value);
        }
        
        #endregion



    }
}
