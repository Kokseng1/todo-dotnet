using api.Dto.UserTask.Category;
using api.Models;

namespace api.Interfaces
{
    public interface CategoryRepositoryInterface
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> DeleteAsync(int id);
        Task<bool> CategoryExists(int id);
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto UpdateCategoryDto);

        Task<Category?> GetByNameAsync(string name);
    }
}