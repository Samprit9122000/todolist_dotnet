using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Models.Data;
using Todolist.Api.Models.DTO;
using Todolist.Api.Responses;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Models.Domain;
using System.Reflection;
using Todolist.Api.Repos;

namespace Todolist.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserRepo userRepo;

        public UserController(AppDbContext db, IUserRepo userRepo)
        {
            this._db = db;
            this.userRepo = userRepo;
        }




        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await userRepo.GetAllAsync();
                return StatusCode(data.status, data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetById/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var data = _db.t_users.FirstOrDefault(x => x.id == id);

            var connection =  _db.Database.GetDbConnection();
            var data = (await connection.QueryAsync(@"select * from public.t_users where id = @id and is_active = true;",
            new
            {
                id
            })).First();
            
            return Ok(data);
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto userData)
        {
            var emailid = userData.email;
            var connection = _db.Database.GetDbConnection();

            var isEmailPresent = (await connection.QueryAsync(@"select id from public.t_users where email = @emailId",
                new
                {
                    emailid
                })).ToList();

            if (isEmailPresent.Count > 0) {
                return StatusCode(400,new
                {
                    data= new { },
                    devMessege = "Bad request",
                    clientMessage = "Bad request"
                });
            }

            // Map the DTO data in model instance
            Users newUser = new Users()
            {
                username = userData.name,
                email = userData.email,
                password = userData.password,
                created_at = DateTime.UtcNow
            };

            Console.WriteLine(newUser.username);
            await _db.t_users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return StatusCode(201, new
            {
                data = new
                {
                    id = newUser.id,
                    name = newUser.username,
                    email = newUser.email,
                    password = newUser.password,
                    created_at = newUser.created_at
                },
                devMessege= "User created successfully",
                clientMessage= "User created successfully"
            });


        }


        [HttpPatch]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
        {
            var DbConnection = _db.Database.GetDbConnection();

            try
            {
                if (dto == null)
                {
                    return BadRequest(new
                    {
                        data = new List<UpdateUserDto>(),
                        devMessage = "Bad Request",
                        clientMessage = "Bad Request"

                    });

                }

                var userToBeModified = _db.t_users.FirstOrDefault(user => user.id == id);
                if (userToBeModified == null) {
                    return NotFound(new
                    {
                        data = new List<UpdateUserDto>(),
                        devMessage = "Not Found",
                        clientMessage = "Not Found"

                    });
                }

                userToBeModified.username = dto.Name == null ? userToBeModified.username : dto.Name;
                userToBeModified.email = dto.Email == null ? userToBeModified.email : dto.Email;
                userToBeModified.password = dto.Password == null ? userToBeModified.password : dto.Password;

                await _db.SaveChangesAsync();
                return Ok(new
                {
                    data = userToBeModified,
                    devMessage = "User Updated successfully",
                    clientMessage = "User Updated successfully"

                });
            }
            catch (Exception ex) {
                return StatusCode(500, new
                {
                    data = new List<UpdateUserDto>(),
                    devMessage= ex.Message,
                    clientMessage = ex.Message
                });
            }

            
        }


        //[HttpPatch]
        //[Route("Delete/{id:Guid}")]
        //public async Task<IActionResult> Delete([FromRoute] Guid id, [FromBody] bool is_active)
        //{

        //}


      


    }
}
