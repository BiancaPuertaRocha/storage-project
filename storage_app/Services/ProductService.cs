﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;

namespace storage_app.Services
{
    internal class ProductService : ServiceBase, IProductService
    {

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = new();

            var _products = await GetValueAsync<List<Product>>("v1/products");

            if (_products != null) 
                products = _products;

            return products;
        }

        public async Task<List<Product>> GetProductsFiltered(string? description = null, string? category = null, int? quantity = null)
        {
            List<Product> products = new();

            Dictionary<string, string> query = new();
            if (description != null) query["description"] = description;
            if (category != null) query["category"] = category;
            if (quantity != null)
            {
                query["quantity"] = quantity.ToString() ?? "";
            }

            var _products = await GetValueAsync<List<Product>>("v1/products/filters", query);

            if (_products != null)
                products = _products;

            return products;
        }

        public async Task<Product?> GetProductById(int Id)
        {
            Product? product = null;
            string Path = String.Concat("v1/products/", Id);

            var _product = await GetValueAsync<Product>(Path);

            if (_product != null)
                product = _product;

            return product;
        }

        public async Task<bool> InsertProduct(Product product)
        {
            return await PostAsync<Product>("/products", product);
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            return await DeleteAsync<Product>("/products", Convert.ToString(Id));
        }

        public async Task<Product?> UpdateProduct(int Id, Product product)
        {
            string Path = String.Concat("v1/products/", Id);
            return await PutAsync<Product>(Path, product);
        }

        public Task<Product> SellProduct(int Id, int quantity, Product product)
        {
            //    var route = String.Concat(Convert.ToString(Id), Convert.ToString(quantity));
            //    return await PutAsync<Product>("/products", route, product);
            throw new NotImplementedException();
        }
    }
}