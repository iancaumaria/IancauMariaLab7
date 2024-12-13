﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Threading.Tasks;
using IancauMariaLab7.Models;
using System.Collections;




namespace IancauMariaLab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
            _database.CreateTableAsync<Shop>().Wait();
        }
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }
        public async Task DeleteProductAsync(Product product)
        {
            await _database.DeleteAsync(product);
        }

        public Task<List<Product>> ProductsAsync => _database.Table<Product>().ToListAsync();

        internal async Task SaveShopListAsync(ShopList slist)
        {
            throw new NotImplementedException();
        }

       
        internal async Task SaveListProductAsync(ListProduct lp)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        internal async Task<IEnumerable> GetListProductsAsync(int iD)
        {
            throw new NotImplementedException();
        }



        public Task<List<Shop>> GetShopsAsync() => _database.Table<Shop>().ToListAsync();
        public Task<int> SaveShopAsync(Shop shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }
        public async Task DeleteShopListAsync(ShopList shopList)
        {
            if (shopList != null)
            {
                await _database.DeleteAsync(shopList); // Metoda de ștergere din baza de date
            }
        }

    }
}

    