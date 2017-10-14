﻿using System;

namespace MyBuyList.DataLayer.DataAdapters
{
    internal class BaseContextDataAdapter<T>
        where T: Shared.Entities.MyBuyListEntities
    {
        private T _dataContext;
        protected T DataContext
        {
            get { return _dataContext; }
        }

        public BaseContextDataAdapter()
        {
            //_dataContext = (T)Activator.CreateInstance(typeof(T), DBUtils.GetConnectionString());
            _dataContext = (T)Activator.CreateInstance(typeof(T));
        }
    }
}