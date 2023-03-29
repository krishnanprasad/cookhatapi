using cookhatAPI.Interface;
using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using cookhatAPI.Connection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
namespace cookhatAPI.DAL
{
    [ExcludeFromCodeCoverage]
    public class RecipeDAL : IRecipe
    {
        //cookhatDBContext _cookhatDBContext;
        //private readonly IDbConnectionFactory _sqlConnection;
        private readonly IDbConnectionFactory _sqlConnection;
        //public CaseDAL(IDbConnectionFactory sqlConnection)
        //{
        //    _sqlConnection = sqlConnection;
        //}
        public RecipeDAL(IDbConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public bool AddRecipe(AddRecipe recipe)
        {
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeCREATE";
                    command.Connection = dbConnection as SqlConnection;

                    //Params

                    command.Parameters.AddWithValue("influencer_id", recipe.influencerid);
                    command.Parameters.AddWithValue("recipename", recipe.recipename);
                    command.Parameters.AddWithValue("video", recipe.videourl);
                    command.Parameters.AddWithValue("steps", System.Text.Json.JsonSerializer.Serialize(recipe.steps));
                    command.Parameters.AddWithValue("ingredients", System.Text.Json.JsonSerializer.Serialize(recipe.ingredient));
                    command.Parameters.AddWithValue("duration", Convert.ToInt32(recipe.duration));
                    command.Parameters.AddWithValue("cuisine", recipe.cuisinetype);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }

        public RecipeDetail GetRecipeDetail(string recipeID)
        {
            var recipeDetail = new RecipeDetail();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeDetailGET";
                    command.Connection = dbConnection as SqlConnection;

                    //Params
                    command.Parameters.AddWithValue("recipeid", recipeID);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                        recipeDetail.recipeid = reader["recipeid"] as string ?? null;
                        recipeDetail.recipename = reader["recipename"] as string ?? null;
                        recipeDetail.recipename = reader["recipename"] as string ?? null;
                        string recipeingredients = reader["recipeingredients"] as string ?? null;
                        recipeDetail.recipeingredients = JsonConvert.DeserializeObject<List<Ingredient>>(recipeingredients);
                        
                        recipeDetail.recipevideosrc = reader["recipevideosrc"] as string ?? null;
                        string recipesteps = reader["recipesteps"] as string ?? null;                        
                        recipeDetail.recipesteps = JsonConvert.DeserializeObject<List<Steps>>(recipesteps);
                        recipeDetail.recipetime = reader["recipetime"] as int ? ??0;
                        recipeDetail.recipecuisine = reader["recipecuisine"] as string ?? null;
                        //recipeDetail.recipefoodtime = reader["recipefoodtime"] as string ?? null;
                        recipeDetail.chefid = reader["chefid"] as string ?? null;
                        recipeDetail.chefname = reader["chefname"] as string ?? null;
                        recipeDetail.chefimgurl = reader["chefimgurl"] as string ?? null;
                        recipeDetail.recipecreateddate = (reader.IsDBNull(recipecreateddate) ? null : DateTime.Now);
                    }
                    return recipeDetail;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }

        public List<RecipeChef> GetRecipeList(string? infid)
        {
            var recipeList = new List<RecipeChef>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeList";
                    command.Connection = dbConnection as SqlConnection;

                    //Params
                    command.Parameters.AddWithValue("inf_id", infid);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //string recipesteps = reader["recipesteps"] as string ?? null;
                       // string recipeingredients = reader["recipeingredients"] as string ?? null;
                        //List<Ingredient> _recipeingredients = JsonConvert.DeserializeObject<List<Ingredient>>(recipeingredients);
                        var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                        recipeList.Add(new RecipeChef()
                        {
                            //var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                            recipeid = reader["recipeid"] as string ?? null,
                            recipename = reader["recipename"] as string ?? null,
                            //recipeingredients = JsonConvert.DeserializeObject<List<Ingredient>>(recipeingredients),
                            recipevideosrc = reader["recipevideosrc"] as string ?? null,
                            //recipesteps = JsonConvert.DeserializeObject<List<Steps>>(recipesteps),
                            recipetime = reader["recipetime"] as int? ?? 0,
                            recipecuisine = reader["recipecuisine"] as string ?? null,
                            chefid = reader["chefid"] as string ?? null,
                            chefname = reader["chefname"] as string ?? null,
                            chefimgurl = reader["chefimgurl"] as string ?? null,
                            totalfollowers = reader["totalfollowers"] as int ? ?? 0,
                            recipecreateddate = (reader.IsDBNull(recipecreateddate) ? null : DateTime.Now),
                        });
                        
                    }
                    return recipeList;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }

        public List<RecipeDetail> GetRecipeSearchList(string? searchRecipe)
        {
            var recipeList = new List<RecipeDetail>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeSearch";
                    command.Connection = dbConnection as SqlConnection;

                    //Params
                    command.Parameters.AddWithValue("search_recipe", searchRecipe);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //string recipesteps = reader["recipesteps"] as string ?? null;
                        // string recipeingredients = reader["recipeingredients"] as string ?? null;
                        //List<Ingredient> _recipeingredients = JsonConvert.DeserializeObject<List<Ingredient>>(recipeingredients);
                        //var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                        recipeList.Add(new RecipeChef()
                        {
                            //var recipecreateddate = reader.GetOrdinal("recipecreateddate");
                            recipeid = reader["recipeid"] as string ?? null,
                            recipename = reader["recipename"] as string ?? null,
                            //recipeingredients = JsonConvert.DeserializeObject<List<Ingredient>>(recipeingredients),
                            //recipevideosrc = reader["recipevideosrc"] as string ?? null,
                            //recipesteps = JsonConvert.DeserializeObject<List<Steps>>(recipesteps),
                            recipetime = reader["recipetime"] as int? ?? 0,
                            //recipecuisine = reader["recipecuisine"] as string ?? null,
                            //chefid = reader["chefid"] as string ?? null,
                            //chefname = reader["chefname"] as string ?? null,
                            //chefimgurl = reader["chefimgurl"] as string ?? null,
                            //totalfollowers = reader["totalfollowers"] as int? ?? 0,
                            //recipecreateddate = (reader.IsDBNull(recipecreateddate) ? null : DateTime.Now),
                        });

                    }
                    return recipeList;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }
    }
}

