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
    public class BlogDAL : IBlog
    {
        private readonly IDbConnectionFactory _sqlConnection;
        public BlogDAL(IDbConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Blogs GetBlogDetail(string blogid)
        {

            //blogListDetailGET
            var blogDetail = new Blogs();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "blogListDetailGET";
                    command.Connection = dbConnection as SqlConnection;

                    //Params
                    command.Parameters.AddWithValue("blogid", blogid);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var updateddate = reader.GetOrdinal("updateddate");
                        blogDetail.blogid = reader["blogid"] as string ?? null;
                        blogDetail.blogtitle = reader["blogtitle"] as string ?? null;
                        blogDetail.blogtitleimage = reader["blogtitleimage"] as string ?? null;
                        blogDetail.content = reader["content"] as string ?? null;
                        blogDetail.metatags = reader["metatags"] as string ?? null;
                        blogDetail.author = reader["author"] as string ?? null;
                        blogDetail.id = reader["id"] as int? ?? 0;
                        blogDetail.updateddate = reader.IsDBNull(updateddate) ? DateTime.Now : DateTime.Now;
                    }
                    return blogDetail;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }

        public List<Blogs> GetRecommendedBlogList()
        {
            //blogListGET
            var blogList = new List<Blogs>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "blogListGET";
                    command.Connection = dbConnection as SqlConnection;

                   
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var updateddate = reader.GetOrdinal("updateddate");
                        blogList.Add(new Blogs()
                        {
                            blogtitleimage = reader["blogtitleimage"] as string ?? null,
                            author = reader["author"] as string ?? null,
                            content = reader["content"] as string ?? null,
                            blogid = reader["blogid"] as string ?? null,
                            metatags = reader["metatags"] as string ?? null,
                            blogtitle= reader["blogtitle"] as string ?? null,
                            id = reader["id"] as int? ?? 0,                            
                            updateddate = reader.IsDBNull(updateddate) ? DateTime.Now : DateTime.Now
                        });

                    }
                }
                return blogList;
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
            
        }
        public List<Blogs> GetTrendingBlogList()
        {
            //blogListGET
            var blogList = new List<Blogs>();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure; 
                    command.CommandText = "blogtrendingListGET";
                    command.Connection = dbConnection as SqlConnection;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var updateddate = reader.GetOrdinal("updateddate");
                        blogList.Add(new Blogs()
                        {
                            blogtitleimage = reader["blogtitleimage"] as string ?? null,
                            author = reader["author"] as string ?? null,
                            content = reader["content"] as string ?? null,
                            blogid = reader["blogid"] as string ?? null,
                            metatags = reader["metatags"] as string ?? null,
                            blogtitle = reader["blogtitle"] as string ?? null,
                            id = reader["id"] as int? ?? 0,
                            updateddate = reader.IsDBNull(updateddate) ? DateTime.Now : DateTime.Now
                        });

                    }
                }
                return blogList;
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }

        }
    }
}
