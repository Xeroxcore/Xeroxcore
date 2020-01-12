using System;
using api.Models;
using Components;
using Components.DbConnection;
using Components.Errorhandler;
using Components.Security;
using Components.DbConnection.Interface;
using api.Database.Interface;
using System.Security.Claims;
using api.Security;
using api.Security.AuthTemplate;

namespace api.Database
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private string McsCon { get; }
        public static DatabaseHelper Instance = new DatabaseHelper();

        public DatabaseHelper()
            => McsCon = AppConfigHelper.Instance.GetDbConnection();

        public ISqlHelper GetMcsConnection()
            => new NpgSqlHelper(McsCon);

        private object CreateClientId(string id)
        {
            return new
            {
                Id = ObjectConverter.ConvertStringToInt(id)
            };
        }
        public SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore)
        {
            return new SqlCommandHelper<T>(data, ignore);
        }

        private T FetchDataFromDB<T>(string key, string querry)
        {
            var sql = GetMcsConnection();
            var id = CreateClientId(key);
            var sqlCommand = CreateSqlCommand<object>(id,"");
            var dataTable = sql.SelectQuery(
                            querry, sqlCommand);
            var databaseInfo = ObjectConverter.ConvertDataTableRowToObject<T>(dataTable.Rows[0]);
            return databaseInfo;
        }

        private ClientDatabase FetchClientDataBase(string dbKey)
            => FetchDataFromDB<ClientDatabase>(dbKey.ToString()
                                , "Select * from database_list where database_id = @id");
        

        private ClientDatabase FetchUserDb(string key)
        {
            var user = FetchDataFromDB<DataExtension>(key
                                , "Select * from database_list where database_id = @id");
            var database = FetchClientDataBase(user.Database_Id.ToString());
            return database;
        }

        private ClientDatabase FetchTokenDb(string key)
        {
            var user = FetchDataFromDB<DataExtension>(key
                                , "Select * from token where database_id = @id");
            var database = FetchClientDataBase(user.Database_Id.ToString());
            return database;
        }

        // change the behaviour of the method.
        // Check if I can set the claimsprinciple during every request.
        public string GetClientDatabase(ClaimsPrincipal User)
        {           
            try
            {
                var claimHelper = new ClaimsHelper(User.Claims);
                var audiance = claimHelper.GetValueFromClaim("aud");
                var connectionString  = "";
                var key = AesEncrypter._instance.DecryptyData(
                    claimHelper.GetValueFromClaim("Key"));
                if(Validation.StringsAreEqual(audiance,"User"))
                    connectionString = FetchUserDb(key).GetConnectionString();
                else
                    connectionString = FetchTokenDb(key).GetConnectionString();

                return connectionString;
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw;
            }
        }
    }
}