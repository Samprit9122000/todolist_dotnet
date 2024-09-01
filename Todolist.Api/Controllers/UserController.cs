using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Models.Data;
using Todolist.Api.Models.DTO;
using Todolist.Api.Responses;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Models.Domain;
using System.Reflection;

namespace Todolist.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            this._db = db;
        }




        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _db.t_users.ToList();
                if (data.Count == 0) {
                    return NotFound();
                }

                return StatusCode(200, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetById/{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var data = _db.t_users.FirstOrDefault(x => x.id == id);

            var connection = _db.Database.GetDbConnection();
            var data = connection.Query(@"select * from public.t_users where id = @id and is_active = true;",
            new
            {
                id
            }).First();
            
            return Ok(data);
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] CreateUserDto userData)
        {
            var emailid = userData.email;
            var connection = _db.Database.GetDbConnection();

            var isEmailPresent = connection.Query(@"select id from public.t_users where email = @emailId",
                new
                {
                    emailid
                }).ToList();

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
            _db.t_users.Add(newUser);
            _db.SaveChanges();
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
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
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

                _db.SaveChanges();
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




      


    }
}
