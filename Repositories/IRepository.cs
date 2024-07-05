﻿using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        int Insert(T t);
        int Update(T t);
        Task<int> InsertAsync(T t);
        Task<int> UpdateAsync(T t);
        int Delete(T t);
    }
}
