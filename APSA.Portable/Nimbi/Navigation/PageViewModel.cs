﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Nimbi.Navigation
{
    public abstract class PageViewModel : NotifyPropertyChangedBase, INavigatingViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        #region INavigatedPage implementation

        public IViewModelNavigation ViewModelNavigation { get; set; }

        #endregion
    }
}