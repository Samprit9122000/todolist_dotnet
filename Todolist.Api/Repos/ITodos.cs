using Todolist.Api.Models.Domain;
using Todolist.Api.Models.DTO;

namespace Todolist.Api.Repos
{
    public interface ITodos
    {
        Task<dynamic> GetAllAsync();
        Task<dynamic> GetAllAsyncByUserId(Guid id);
        Task<dynamic> BulkCreateAsync(List<CreateTodosDto> todoData);
        //Task<dynamic> UpdateByIdAsync();
        //Task<dynamic> DeleteByIdAsync();




    }
}
