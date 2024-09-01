using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Data.Common;
using Todolist.Api.Models.Data;
using Todolist.Api.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todolist.Api.Repos
{
    public class SqlUserRepo : IUserRepo
    {
        AppDbContext _db; 
        public SqlUserRepo(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<dynamic> GetAllAsync()
        {
            using (var connection = _db.Database.GetDbConnection())  // start of using block
            {
                await connection.OpenAsync();
                try
                {
                    var data = (await connection.QueryAsync("select * from public.t_users where is_active = true;")).ToList();
                    if (data.Count == 0)
                    {
                        return new
                        {
                            status = 404,
                            success = true,
                            data = new List<Users>(),
                            clientMessege = "User data fetched but empty",
                        };
                    }

                    return new
                    {
                        status = 200,
                        success = true,
                        data,
                        clientMessege = "User data fetched",
                    };

                }
                catch (Exception ex)
                {
                    return new
                    {
                        status = 500,
                        success = false,
                        data=new List<dynamic>(),
                        clientMessege = ex.Message,
                    };
                }

            }// end of using block
        }
    }
}
