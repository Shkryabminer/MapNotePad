﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapNotePad.Controls
{
   public class CustomSearchBar : SearchBar
    {
        public static BindableProperty CornerRadiusProperty =
           BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CommonEntry), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
