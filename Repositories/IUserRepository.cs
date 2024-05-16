﻿using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IDisposable, IRepository<SystemUser>
    {
        SystemUser? FindById(int id);
        List<SystemUser> FindByName(string username);
        SystemUser? FindByNameAndPassword(string username, string password);
    }
}
