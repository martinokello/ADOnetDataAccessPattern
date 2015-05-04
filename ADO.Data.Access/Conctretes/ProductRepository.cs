using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO.Data.Access.Domain;
using ADO.Data.Access.Interfaces;


namespace ADO.Data.Access.Conctretes
{
    public class ProductRepository : IRepository<int?, Product>
    {
        private const string _getAllProducts = @"select * from Product";
        private const string _getProductById = @"select * from Product where ProductId=@ProductId";
        private const string _addProduct = @"insert into Product (ProductName,ProductDescription,ProductPrice) values (@ProductName,@ProductDescription,@ProductPrice)";
        private const string _deleteProduct = @"delete from Product where ProductId=@ProductId";
        private const string _updateProduct = @"update Product set 
                                                ProductName=@ProductName, 
                                                ProductDescription=@ProductDescription,
                                                ProductPrice=@ProductPrice
                                                where ProductId=@ProductId";

        private DataAccessAdapter<Product> _productDataAccessAdapter;

        public ProductRepository()
        {
            _productDataAccessAdapter = new DataAccessAdapter<Product>();
        }

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ADONETConnectionString"].ConnectionString;
            }
        }
        public Product[] GetAll()
        {
            var products = new List<Product>();
            using (var con = new SqlConnection(ConnectionString))
            {
                var cmd = con.CreateCommand();

                cmd.CommandText = _getAllProducts;
                con.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = (int?)(reader["ProductId"] ?? -1),
                        ProductName = (string)reader["ProductName"],
                        ProductDescription = (string)reader["ProductDescription"],
                        ProductPrice = (decimal?)(reader["ProductPrice"] ?? 0.00)
                    };
                    products.Add(product);
                }
                con.Close();
            }

            return products.ToArray();
        }

        public Product GetById(int? key)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = _getProductById;
                        var product = new Product { ProductId = key };
                        _productDataAccessAdapter.SetParameters(product);

                        cmd.Parameters.AddRange(
                            _productDataAccessAdapter.GetParameters().Where(p => p.Value != null).ToArray());

                        con.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            product.ProductId = (int?)(reader["ProductId"] ?? -1);
                            product.ProductName = (string)reader["ProductName"];
                            product.ProductDescription = (string)reader["ProductDescription"];
                            product.ProductPrice = (decimal?)(reader["ProductPrice"] ?? 0.00);

                        }

                        con.Close();
                        return product;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Add(Product instance)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = _addProduct;
                        _productDataAccessAdapter.SetParameters(instance);

                        cmd.Parameters.AddRange(
                            _productDataAccessAdapter.GetParameters().Where(p => p.Value != null).ToArray());

                        con.Open();

                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int? key)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = _deleteProduct;
                        var product = new Product { ProductId = key };

                        _productDataAccessAdapter.SetParameters(product);

                        cmd.Parameters.AddRange(
                            _productDataAccessAdapter.GetParameters().Where(p => p.Value != null).ToArray());

                        con.Open();

                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }

                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Product instance)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = _updateProduct;
                        _productDataAccessAdapter.SetParameters(instance);
                        var parameters =
                            _productDataAccessAdapter.GetParameters()
                                .Where(p => p.Value != null && p.Value != string.Empty)
                                .ToArray();
                        cmd.Parameters.AddRange(
                            parameters);

                        con.Open();

                        var reader = cmd.ExecuteReader();
                        con.Close();
                        return true;
                    }

                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
