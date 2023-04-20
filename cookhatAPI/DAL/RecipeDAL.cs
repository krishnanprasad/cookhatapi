﻿using cookhatAPI.Interface;
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

                    command.Parameters.AddWithValue("chef_id", recipe.influencerid);
                    command.Parameters.AddWithValue("recipename", recipe.recipename);
                    command.Parameters.AddWithValue("video", recipe.videourl);
                    command.Parameters.AddWithValue("steps", System.Text.Json.JsonSerializer.Serialize(recipe.steps));
                    command.Parameters.AddWithValue("ingredients", System.Text.Json.JsonSerializer.Serialize(recipe.ingredient));
                    command.Parameters.AddWithValue("duration", Convert.ToInt32(recipe.duration));
                    command.Parameters.AddWithValue("type", Convert.ToInt32(recipe.type));
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
                        recipeDetail.recipeimage = reader["recipeimage"] as string ?? null;
                        string recipesteps = reader["recipesteps"] as string ?? null;                        
                        recipeDetail.recipesteps = JsonConvert.DeserializeObject<List<Steps>>(recipesteps);
                        
                        recipeDetail.recipetime = reader["recipetime"] as int ? ??0;
                        recipeDetail.recipecuisine = reader["recipecuisine"] as string ?? null;
                        recipeDetail.recipetypeimgsrc = reader["recipetypeimgsrc"] as string ?? null;
                        recipeDetail.recipetypename = reader["recipetypename"] as string ?? null;
                        recipeDetail.recipeimage = reader["recipeimage"] as string ?? null;
                        recipeDetail.recipetypeid = reader["recipetypeid"] as int? ?? 0;
                        recipeDetail.chefid = reader["chefid"] as string ?? null;
                        recipeDetail.chefname = reader["chefname"] as string ?? null;
                        recipeDetail.chefimgurl = reader["chefimgurl"] as string ?? null;
                        recipeDetail.totalrecipes = reader["totalrecipes"] as int? ?? 0;
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

        public List<RecipeChef> GetRecipeList(string? chefid)
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
                    command.Parameters.AddWithValue("chef_id", chefid);

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
                            recipeimage = reader["recipeimage"] as string ?? null,
                        recipetypeimgsrc = reader["recipetypeimgsrc"] as string ?? null,
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

        public List<RecipeDetail> GetRecipeSearchList(string recipesearch, string ingredient, string category,string cuisinetype)
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
                    if(recipesearch!=null)
                    command.Parameters.AddWithValue("@recipename_filter", recipesearch);
                    if (category != null)
                        command.Parameters.AddWithValue("category_filter", category);
                    if (ingredient != null)
                        command.Parameters.AddWithValue("ingredient_filter", ingredient);
                    if (cuisinetype != null)
                        command.Parameters.AddWithValue("cuisinetype_filter", cuisinetype);

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
                            recipetime = reader["recipetime"] as int? ?? 0,
                            recipetypeimgsrc = reader["recipetypeimgsrc"] as string ?? null,
                            recipeimage = reader["recipeimage"] as string ?? null,
                        chefid = reader["chefid"] as string ?? null,
                            chefname = reader["chefname"] as string ?? null,
                            chefimgurl = reader["chefimgurl"] as string ?? null,
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
        public List<RecipeChef> GetTrendingRecipeList()
        {
            var recipeList = new List<RecipeChef>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeTrendingList";
                    command.Connection = dbConnection as SqlConnection;

                    
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
                            recipeimage = reader["recipeimage"] as string ?? null,
                            recipetypeimgsrc = reader["recipetypeimgsrc"] as string ?? null,
                            chefid = reader["chefid"] as string ?? null,
                            chefname = reader["chefname"] as string ?? null,
                            chefimgurl = reader["chefimgurl"] as string ?? null,
                            totalfollowers = reader["totalfollowers"] as int? ?? 0,
                            totalrecipes= reader["totalrecipes"] as int? ?? 0,
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
        public List<RecipeChef> GetSimilarRecipeList()
        {
            var recipeList = new List<RecipeChef>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "recipeSmilarList";
                    command.Connection = dbConnection as SqlConnection;


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
                            recipeimage = reader["recipeimage"] as string ?? null,
                            recipetypeimgsrc = reader["recipetypeimgsrc"] as string ?? null,
                            chefid = reader["chefid"] as string ?? null,
                            chefname = reader["chefname"] as string ?? null,
                            chefimgurl = reader["chefimgurl"] as string ?? null,
                            totalfollowers = reader["totalfollowers"] as int? ?? 0,
                            totalrecipes = reader["totalrecipes"] as int? ?? 0,
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
        public List<String> GetCuisineSearchList()
        {
            var recipeList = new List<string>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "CuisineForSearchlistget";
                    command.Connection = dbConnection as SqlConnection;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        recipeList.Add(reader["cuisine"] as string ?? null);

                    }
                    return recipeList;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting GetCuisineSearchList:: " + e.Message, e.InnerException);
            }
        }
    }
}

