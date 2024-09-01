using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todolist.Api.Models.Data;
using Todolist.Api.Models.Domain;
using Todolist.Api.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todolist.Api.Repos
{
    public class SqlTodosRepo : ITodos
    {
        private readonly AppDbContext _db;
        public SqlTodosRepo(AppDbContext db)
        {
            this._db = db;   
        }

        public async Task<dynamic> GetAllAsync()
        {
            using(var connection = _db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                try
                {
                    var data = (await connection.QueryAsync(@"select * from public.func_get_todos_all()")).ToList();

                    if(data.Count == 0)
                    {
                        return new
                        {
                            status = 200,
                            success = true,
                            data,
                            clientMessage = "Todos data fetched but empty"
                        };
                    }

                    return new
                    {
                        status = 200,
                        success = true,
                        data,
                        clientMessage = "Todos data fetched successfully"
                    };

                }
                catch (Exception ex)
                {
                    return new
                    {
                        status = 500,
                        success = false,
                        data = new List<Todos>(),
                        clientMessage = ex.Message
                    };
                }
            }
        }


        public async Task<dynamic> GetAllAsyncByUserId(Guid userid)
        {
            using (var connection = _db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                try
                {
                    var data = (await connection.QueryAsync(@"select * from public.func_get_todos_by_user(@userid)", new
                    {
                        userid

                    })).ToList();

                    if (data.Count == 0)
                    {
                        return new
                        {
                            status = 200,
                            success = true,
                            data,
                            clientMessage = "Todos data fetched but empty"
                        };
                    }

                    return new
                    {
                        status = 200,
                        success = true,
                        data,
                        clientMessage = "Todos data fetched successfully"
                    };

                }
                catch(Exception ex)
                {
                    return new
                    {
                        status = 500,
                        success = false,
                        data = new List<Todos>(),
                        clientMessage = ex.Message
                    };
                }
            }

        }
        public async Task<dynamic> BulkCreateAsync(List<CreateTodosDto> todoData)
        {
            try
            {
                List<Todos> todos = new List<Todos>();
                // map dto data to model
                foreach (var todo in todoData)
                {
                    todos.Add(new Todos()
                    {
                        user_id = todo.user_id,
                        todo = todo.todo,
                    });
                }

                await _db.AddRangeAsync(todos);
                await _db.SaveChangesAsync();

                // object to return
                List<GetTodosDto> returnTodos = new List<GetTodosDto>();
                foreach (var todo in todos)
                {
                    returnTodos.Add(new GetTodosDto()
                    {
                        id = todo.id,
                        user_id=todo.user_id,
                        todo = todo.todo,   
                        is_active = todo.is_active
                    });
                }

                return new
                {
                    status = 200,
                    success = true,
                    data = returnTodos,
                    clientMessage = "Todo data inserted successfully."
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    status = 500,
                    success = false,
                    data = new List<Todos>(),
                    clientMessage = ex.Message
                };
            }
        }
        
        //Task<dynamic> UpdateByIdAsync();
        //Task<dynamic> DeleteByIdAsync();
    }
}
