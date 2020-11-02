using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Validators
{
    public class PinValidator : IPinValidator
    {
        #region --IpinValidator implementation--

       
        public bool PinModelIsValid(PinModel model)
        {
            bool isValid;

            isValid = LocationIsValid(model.Latitude) && LocationIsValid(model.Longtitude) && NameIsValid(model.Name);
            
            return isValid;
        }
        #endregion

        #region --Private helpers--

        private bool LocationIsValid(double location)
        {
            return location >= 0 && location <= 360;
        }

        private bool NameIsValid(string name)
        {
            return  !string.IsNullOrEmpty(name);
        }
        #endregion
    }
}
