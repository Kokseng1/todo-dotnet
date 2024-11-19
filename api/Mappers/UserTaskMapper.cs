using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.UserTask;
using api.Models;

namespace api.Mappers
{
    public static class UserTaskMapper
    {
        public static UserTaskDto ToUserTaskDto(this UserTask userTask) {
            return new UserTaskDto {
                Id = userTask.Id,
                CategoryId = userTask.CategoryId,
                name = userTask.name,
                status = userTask.status
            };
        }

        public static UserTask ToUserTaskFromRequestDto(this CreateUserTaskRequestDto createUserTaskRequestDto)
        {
            return new UserTask {
                CategoryId = createUserTaskRequestDto.CategoryId,
                name = createUserTaskRequestDto.name,
                status = createUserTaskRequestDto.status
            };
        }
    }
}