// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using Microsoft.Phone.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// Questa classe serve per creare i gruppi per il Long List Selector
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class KeyedList<TKey, TItem> : List<TItem>
    {
        
        public TKey Key { protected set; get; }

        public KeyedList(TKey key, IEnumerable<TItem> items): base(items)
        {
            Key = key;
        }

        public KeyedList(IGrouping<TKey, TItem> grouping): base(grouping)
        {
            Key = grouping.Key;
        }
        
    }
}
