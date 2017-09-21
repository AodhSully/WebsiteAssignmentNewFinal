using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Web.Configuration;

namespace WebsiteAssignmentNew.Models
{
    public class DAO
    {
        SqlConnection conn;
        public string message = "";

        public void Connection()
        {
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString);
        }

        //method for inserting data to database
        public int Insert(UserModel user)
        {
            //count shows number of affected rows
            int count = 0;
            SqlCommand cmd;
            string password;

            Connection();
            cmd = new SqlCommand("uspCreateUserTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
           

            password = Crypto.HashPassword(user.Password);
            cmd.Parameters.AddWithValue("@Pass", password);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;

        }

        public int InsertAddress(ShippingModel address)
        {
            //count shows number of affected rows
            int count = 0;
            SqlCommand cmd;
            //string password;

            Connection();
            cmd = new SqlCommand("uspDeliveryAddressUserTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Address1", address.Address1);
            cmd.Parameters.AddWithValue("@Address2", address.Address2);
            cmd.Parameters.AddWithValue("@Address3", address.Address3);
            cmd.Parameters.AddWithValue("@City", address.City);
            cmd.Parameters.AddWithValue("@Country", address.Country);
            cmd.Parameters.AddWithValue("@PostCode", address.PostCode);


            //password = Crypto.HashPassword(user.Password);
            //cmd.Parameters.AddWithValue("@Pass", password);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;

        }

        public int getAddressID(string a, string b)
        {
            int result = 0;
            //int count = 0;
            SqlCommand cmd;
            SqlDataReader reader;
            //string password;

            Connection();
            cmd = new SqlCommand("select AddressID from DeliveryAddress where Address1 = '" + a + "' and Address2 = '" + b + "'", conn);
          

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["AddressID"].ToString());


                }

            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return result;

        }
        //Method for checking login
        public string CheckLogin(UserModel user)
        {
            string firstName = null;
            SqlCommand cmd;
            SqlDataReader reader;
            string password;
            Connection();
            cmd = new SqlCommand("uspCheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", user.UserName);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Pass"].ToString();
                    
                    if (Crypto.VerifyHashedPassword(password, user.Password))
                    {
                        firstName = reader["FirstName"].ToString();
                    }

                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (FormatException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return firstName;
        }
        public List<Track> ShowAllTracks()
        {
            List<Track> trackList = new List<Track>();
            SqlCommand cmd;
            SqlDataReader reader;
            //Calling connection method to establish connection string
            Connection();
            cmd = new SqlCommand("uspAllTunes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Track track = new Track();
                    track.TrackCode = reader["TrackID"].ToString();
                    track.Artist = reader["Artist"].ToString();
                    track.Title = reader["Track"].ToString();
                    //track.Quantity = int.Parse(reader["QuantityAvailable"].ToString());
                    track.PriceVinyl = decimal.Parse(reader["Price"].ToString());
                    track.Artwork = reader["Artwork"].ToString();
                    track.TrackAudio = reader["Audio"].ToString();
                    trackList.Add(track);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return trackList;
        }

        public List<Shop> ShowAllMerchandise()
        {
            List<Shop> shopList = new List<Shop>();
            SqlCommand cmd;
            SqlDataReader reader;
            //Calling connection method to establish connection string
            Connection();
            cmd = new SqlCommand("uspAllMerchandise", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Shop merchandise = new Shop();
                    merchandise.ItemCode = reader["ItemCode"].ToString();
                    merchandise.Item = reader["Item"].ToString();
                    merchandise.Description = reader["ItemDescription"].ToString();
                    merchandise.Price = decimal.Parse(reader["Price"].ToString());
                    merchandise.ItemImage = reader["ItemImage"].ToString();
                    shopList.Add(merchandise);
                   
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return shopList;
        }
        public int AddTransaction(/*string transactionId,*/ DateTime date, decimal totalprice,
            string UserName)
        {
            int count = 0;
            SqlCommand cmd;

            Connection();
       
            cmd = new SqlCommand("uspInsertCustomerOrderTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dateordered", date);
            cmd.Parameters.AddWithValue("@cost", totalprice);
            //cmd.Parameters.AddWithValue("@addressid", addressid);
            cmd.Parameters.AddWithValue("@Username", UserName);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;

        }
        public int AddTransactionItem(/*string transactionId,*/ ProductModel item)
        {
            int count = 0;
            SqlCommand cmd;

            Connection();

            cmd = new SqlCommand("uspOrderProductItem", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@orderId", transactionId);
            //cmd.Parameters.AddWithValue("@OrderProductId", item.ProductID);
            //cmd.Parameters.AddWithValue("@OrderId", item.ProductID);
            cmd.Parameters.AddWithValue("@ProductId", item.ProductID);
            cmd.Parameters.AddWithValue("@quantity", item.Quantity);


            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }
    }

}

