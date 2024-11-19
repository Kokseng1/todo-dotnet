using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.UserTask;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserTaskRepository : UserTaskRepositoryInterface
    {
        private readonly ApplicationDBContext _context;
        public UserTaskRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<UserTask> CreateAsync(UserTask UserTaskModel)
        {
            await _context.UserTasks.AddAsync(UserTaskModel);
            await _context.SaveChangesAsync();
            return UserTaskModel;
        }

        public async Task<UserTask?> DeleteAsync(int id)
        {
            var UserTaskModel = await _context.UserTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (UserTaskModel == null)
            {
                return null;
            }

            _context.UserTasks.Remove(UserTaskModel);
            await _context.SaveChangesAsync();
            return UserTaskModel;
        }

        public Task<List<UserTask>> GetAllAsync(QueryObject queryObject)
        {
            var userTasks = _context.UserTasks.Include(t => t.Category).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.UserTaskName)) {
                userTasks = userTasks.Where(t => t.name.Contains(queryObject.UserTaskName));
            }
            return userTasks.ToListAsync();
        }
    

        public async Task<UserTask?> GetByIdAsync(int id)
        {
            return await _context.UserTasks.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<UserTask?> GetByStatusAsync(bool status)
        {
            return await _context.UserTasks.FirstOrDefaultAsync(s => s.status == status);
        }

        public Task<bool> UserTaskExists(int id)
        {
            return _context.UserTasks.AnyAsync(s => s.Id == id);
        }

        public async Task<UserTask?> UpdateAsync(int id, UpdateUserTaskDto UserTaskDto)
        {
            var existingUserTask = await _context.UserTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUserTask == null)
                {
                    return null;
                }

            existingUserTask.CategoryId = UserTaskDto.CategoryId;
            existingUserTask.name = UserTaskDto.name;
            existingUserTask.status = UserTaskDto.status;
            existingUserTask.CreatedOn = UserTaskDto.CreatedOn;
            await _context.SaveChangesAsync();

            return existingUserTask;
        }    
    }
}