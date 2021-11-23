﻿using System;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    public interface IRepository
    {
        public void CheckTable();
    }

    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="T">Business Model class</typeparam>
    public interface IRepository<T> : IRepository
    {
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetByField(string field, string value);
        //T FindById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }


}
