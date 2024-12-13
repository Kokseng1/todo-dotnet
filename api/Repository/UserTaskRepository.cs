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
        private readonly CategoryRepositoryInterface _categoryRepositoryInterface;
        public UserTaskRepository(ApplicationDBContext context, CategoryRepositoryInterface categoryRepositoryInterface)
        {
            _categoryRepositoryInterface = categoryRepositoryInterface;
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

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy)) {
                    userTasks = queryObject.SortBy.Equals("name") ? userTasks.OrderByDescending(t => t.name) :  userTasks.OrderByDescending(t => t.CreatedOn);
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return userTasks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }
    

        public async Task<UserTask?> GetByIdAsync(int id)
        {
            return await _context.UserTasks.Include(ut => ut.Category).FirstOrDefaultAsync(i => i.Id == id);
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
            var existingUserTask = await _context.UserTasks.Include(ut => ut.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (existingUserTask == null)
                {
                    return null;
                }

            if (UserTaskDto.CategoryId == null) {
                // Console.WriteLine("in cat id null " + UserTaskDto.categoryName);
                var category =  await _categoryRepositoryInterface.GetByNameAsync(UserTaskDto.categoryName);
                UserTaskDto.CategoryId = category.Id;
            }

            existingUserTask.CategoryId = UserTaskDto.CategoryId;
            // Console.WriteLine("after setting cat id " + existingUserTask.CategoryId);
            existingUserTask.name = UserTaskDto.name;
            existingUserTask.status = UserTaskDto.status;
            await _context.SaveChangesAsync();

            return existingUserTask;
        }    
    }
}