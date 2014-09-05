﻿using System.Windows;
using System.Windows.Controls;

namespace WinnerSV.Helpers
{
    /// <summary>
    /// Classe che aggiunge la dependency property SelectedItem sul controllo LongListSelector
    /// </summary>
    public class LongListSelector : Microsoft.Phone.Controls.LongListSelector
    {
        public LongListSelector()
        {
            SelectionChanged += LongListSelector_SelectionChanged;
        }

        void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = base.SelectedItem;
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(LongListSelector),
            new PropertyMetadata(null, OnSelectedItemChanged)
            );

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selector = (LongListSelector)d;
            selector.SetSelectedItem(e);
        }

        private void SetSelectedItem(DependencyPropertyChangedEventArgs e)
        {
            base.SelectedItem = e.NewValue;
        }

        public new object SelectedItem
        {
            get 
            { 
                return GetValue(SelectedItemProperty); 
            }

            set 
            { 
                SetValue(SelectedItemProperty, value); 
            }
        }
    }
}
