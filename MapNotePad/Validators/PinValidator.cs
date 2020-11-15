using MapNotePad.Models;
using MapNotePad.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Validators
{
    public class PinValidator
    {
        #region --IpinValidator implementation--

       
        public bool PinModelIsValid(PinModelViewModel model)
        {
            bool isValid;

            isValid = LatitudeIsValid(model.Latitude) && LongitudeIsValid(model.Longtitude) && NameIsValid(model.Name);
            
            return isValid;
        }
        #endregion

        #region --Private helpers--

        private bool LatitudeIsValid(double location)
        {
            return location >= -90 && location <= 90;
        }

        private bool LongitudeIsValid(double location)
        {
            return location >= -180 && location <= 180;
        }

        private bool NameIsValid(string name)
        {
            return  !string.IsNullOrEmpty(name);
        }
        #endregion
    }
}
