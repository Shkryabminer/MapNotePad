using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Pickers
{
   public interface IPinModelsPicker
    {
        List<PinModel> Pick(List<PinModel> list, string input);
    }
}
