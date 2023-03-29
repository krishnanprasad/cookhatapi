using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookhatAPI.Connection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.Common;
using cookhatAPI.Interface;

namespace cookhatAPI.DAL
{
    public class IngredientDAL:IIngredient
    {
        private readonly IDbConnectionFactory _sqlConnection;
        public IngredientDAL(IDbConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public List<Ingredient> GetRecommendedIngredientList()
        {
            var ingredientList = new List<Ingredient>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "ingredientListRecommended";
                    command.Connection = dbConnection as SqlConnection;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                       ingredientList.Add(new Ingredient()
                        {
                            //var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                            ingredientID = reader["ingredientid"] as string ?? null,
                            ingredientImage = reader["ingredientimage"] as string ?? null,
                            ingredientName = reader["ingredientname"] as string ?? null,
                            
                        });

                    }
                    return ingredientList;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error Getting Ingredient List: " + e.Message, e.InnerException);
            }
        }
        public List<Ingredient> GetIngredientListSearch(string? searchterm)
        {
            var ingredientList = new List<Ingredient>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "ingredientListGet";
                    command.Connection = dbConnection as SqlConnection;
                    if (searchterm != null) { 
                    command.Parameters.AddWithValue("searchterm", searchterm);
                    }
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ingredientList.Add(new Ingredient()
                        {
                            //var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                            ingredientID = reader["ingredientid"] as string ?? null,
                            ingredientImage = reader["ingredientimage"] as string ?? null,
                            ingredientName = reader["ingredientname"] as string ?? null,

                        });

                    }
                    return ingredientList;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error Getting Ingredient List: " + e.Message, e.InnerException);
            }
        }

    }
}
