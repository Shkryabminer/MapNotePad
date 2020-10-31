using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services
{
    public interface ISettingsManager
    {


        int AutorizatedUserId { get; set; }
        int SelectedSortMethode { get; set; }
        string LanguageSource { get; set; }
     //   string ThemaSource { get; set; }

    }
}
