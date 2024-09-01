using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Models.Domain;
using Todolist.Api.Models.DTO;
using Todolist.Api.Repos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todolist.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodosController : ControllerBase
    {
        private readonly ITodos todosRepo;
        public TodosController(ITodos todo) 
        {
            this.todosRepo = todo;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await todosRepo.GetAllAsync();
                return StatusCode(data.status, data);
            }  
            catch(Exception ex)
            {
                return StatusCode(500,new {
                        status = 500,
                        success = false,
                        data = new List<CreateTodosDto>(),
                        clientMessage = ex.Message
                    });
            }

        }

        [HttpGet]
        [Route("GetByUserId/{id:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid id)
        {
            try
            {
                var data = await todosRepo.GetAllAsyncByUserId(id);
                return StatusCode(data.status, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    success = false,
                    data = new List<CreateTodosDto>(),
                    clientMessage = ex.Message
                });
            }

        }


        [HttpPost]
        [Route("BulkCreate")]
        public async Task<IActionResult> BulkCreate([FromBody] List<CreateTodosDto> todoData)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data = await todosRepo.BulkCreateAsync(todoData);
                    return StatusCode(data.status, data);
                }
                else
                {
                    return StatusCode(400, new
                    {
                        status = 500,
                        success = false,
                        data = new List<CreateTodosDto>(),
                        clientMessage = ModelState
                    });

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    success = false,
                    data = new List<CreateTodosDto>(),
                    clientMessage = ex.Message
                });
            }
        }


    }
}
