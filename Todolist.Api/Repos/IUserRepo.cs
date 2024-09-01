namespace Todolist.Api.Repos
{
    public interface IUserRepo
    {
        Task<dynamic> GetAllAsync();
    }
}
