using cookhatAPI.Interface;
using cookhatAPI.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookhatAPI.Connection;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace cookhatAPI.DAL
{
    public class ChefDAL : IChef
    {
        private readonly IDbConnectionFactory _sqlConnection;
        public ChefDAL(IDbConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public bool CreateChef(Credentials chefcredentials)
        {
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "influencercredentialsCREATE";
                    command.Connection = dbConnection as SqlConnection;

                    //Params

                    command.Parameters.AddWithValue("email", chefcredentials.email);
                    command.Parameters.AddWithValue("password", chefcredentials.password);
                    SqlParameter outParam = command.Parameters.Add("@responseMessage", SqlDbType.VarChar,-1);
                    outParam.Direction = ParameterDirection.Output;
                    using (SqlDataReader reader = command.ExecuteReader())
                    { 
                        while (reader.Read())
                    {

                    }
                    }
                    var Message = (string)command.Parameters["@responseMessage"].Value;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }

        public ChefDetail GetChefDetail(string chefid)
        {
            var chefDetail = new ChefDetail();
            try
            {

                using (DbConnection dbConnection = _sqlConnection.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "InfluencerDetailGET";
                    command.Connection = dbConnection as SqlConnection;

                    //Params
                    command.Parameters.AddWithValue("infid", chefid);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var chefaccountcreateddate = reader.GetOrdinal("chefaccountcreateddate");
                        chefDetail.chefid = reader["chefid"] as string ?? null;
                        chefDetail.chefname = reader["chefname"] as string ?? null;
                        chefDetail.chefimgurl = reader["chefimgurl"] as string ?? null;
                        chefDetail.cheffavouritecuisine = reader["cheffavouritecuisine"] as string ?? null;
                        chefDetail.chefcaption = reader["chefcaption"] as string ?? null;
                        chefDetail.cheflocation = reader["cheflocation"] as string ?? null;
                        chefDetail.totalrecipes = reader["totalrecipes"] as int ? ??0;
                        chefDetail.totalfollowers = reader["totalfollowers"] as int? ?? 0;
                        chefDetail.totalproducts = reader["totalproducts"] as int? ?? 0;
                        chefDetail.chefaccountcreateddate = reader.IsDBNull(chefaccountcreateddate) ? DateTime.Now : DateTime.Now;
                    }
                    return chefDetail;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error creating new Case:: " + e.Message, e.InnerException);
            }
        }
    }
}
