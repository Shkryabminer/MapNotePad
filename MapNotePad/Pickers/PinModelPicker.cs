using System.Collections.Generic;
using MapNotePad.ViewModels;
using System.Diagnostics;

namespace MapNotePad.Pickers
{
    public static class PinModelPicker 
    {
        #region --IPinModelsPicker implementation--

        public static List<PinModelViewModel> Pick(this IEnumerable<PinModelViewModel> collection, string input)
        {
            List<PinModelViewModel> newList = new List<PinModelViewModel>();

            foreach (PinModelViewModel pinModelVM in collection)
            {
                if (string.IsNullOrEmpty(pinModelVM.KeyWords))
                {
                    pinModelVM.KeyWords = "";
                }
                else 
                {
                    Debug.WriteLine("PinVM has keywords");
                }

                if (pinModelVM.Name.ToLower().Contains(input.ToLower()) ||
                    pinModelVM.KeyWords.ToLower().Contains(input.ToLower()) ||
                    pinModelVM.Latitude.ToString().Contains(input) ||
                    pinModelVM.Longtitude.ToString().Contains(input))
                {
                    newList.Add(pinModelVM);
                }
            }

            return newList;
        }

        #endregion
    }
}
