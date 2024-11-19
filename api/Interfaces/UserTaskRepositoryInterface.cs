using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.UserTask;
using api.Models;

namespace api.Interfaces
{
    public interface UserTaskRepositoryInterface
    {
        Task<List<UserTask>> GetAllAsync();
        Task<UserTask?> GetByIdAsync(int id);
        Task<UserTask?> GetByStatusAsync(bool status);
        Task<UserTask> CreateAsync(UserTask UserTaskModel);
        Task<UserTask?> UpdateAsync(int id, UpdateUserTaskDto UpdateUserTaskDto);
        Task<UserTask?> DeleteAsync(int id);
        Task<bool> UserTaskExists(int id);
        
    }
}