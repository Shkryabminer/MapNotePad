using MapNotePad.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Pickers
{
    public class PinModelPicker : IPinModelsPicker
    {
        #region --IPinModelsPicker implementation--
        public List<PinModel> Pick(List<PinModel> list, string input)
        {
            List<PinModel> newList = new List<PinModel>();

            foreach (PinModel e in list)
            {
                e.KeyWords = "";

                if (e.Name.Contains(input) ||
                    e.KeyWords.Contains(input) ||
                    e.Latitude.ToString().Contains(input) ||
                    e.Longtitude.ToString().Contains(input))
                {
                    newList.Add(e);
                }
            }
            return newList;
        }
        #endregion

    }
}
