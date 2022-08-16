using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EntityLayer;

namespace DataLinkLayer
{
    public class Product_sql_connect
    {
        SqlConnection conn;

        public Product_sql_connect()
        {
            conn = new SqlConnection("Server = DEL1-LHP-N82143\\MSSQLSERVER01; Database = Assessment; Integrated Security = SSPI");
        }

        public void ReadDataAllProducts()
        {
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(@"Select pro.ProductId,pro.ProductName,pro.Price,pro.CategoryId,cat.CategoryName 
                            from Product pro left join Category cat on pro.CatgeoryId = cat.CategoryId", conn);

                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)} : {reader[i]}");
                    }
                    Console.WriteLine("".PadLeft(15, '-'));
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool InsertDataIntoProduct(Product p)
        {
            try
            {
                conn.Open();
                string insertString = $"Insert into Product values('{p.ID}','{p.Name}','{p.Price}','{p.CategoryID}')";

                SqlCommand cmd = new SqlCommand(insertString, conn);

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool UpdateProdcutPrice(int id, int price)
        {
            try
            {
                conn.Open();

                string UpdateString = $"update Product set Price = '{price}' where ProductId = '{id}";

                SqlCommand cmd = new SqlCommand(UpdateString, conn);

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool UpdateProductName(int id, string name)
        {
            try
            {
                conn.Open();

                string UpdateString = $"update Product set ProductName = '{name}' where ProductId = '{id}'";

                SqlCommand cmd = new SqlCommand(UpdateString, conn);

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool DeleteData(int id)
        {
            try
            {
                conn.Open();

                string DeleteString = $"delete from Product where ProductId = '{id}'";

                SqlCommand cmd = new SqlCommand(DeleteString, conn);

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int GetNumberOfRecords()
        {
            int count = -1;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from Product", conn);

                count = (int)cmd.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return count;
        }
    }
}

