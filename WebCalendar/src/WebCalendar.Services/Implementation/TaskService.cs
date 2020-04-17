using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Task;

namespace WebCalendar.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public TaskService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddAsync(TaskCreationServiceModel entity)
        {
            DAL.Models.Entities.Task task = _mapper.Map<TaskCreationServiceModel, DAL.Models.Entities.Task>(entity);
            await _uow.GetRepository<DAL.Models.Entities.Task>().AddAsync(task);

            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskServiceModel>> GetAllAsync()
        {
            IEnumerable<DAL.Models.Entities.Task> tasks = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetAllAsync();

            IEnumerable<TaskServiceModel> taskServiceModels = _mapper
                .Map<IEnumerable<DAL.Models.Entities.Task>, IEnumerable<TaskServiceModel>>(tasks);

            return taskServiceModels;
        }

        public async Task<TaskServiceModel> GetByIdAsync(Guid id)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetByIdAsync(id);
            TaskServiceModel taskServiceModel = _mapper.Map<DAL.Models.Entities.Task, TaskServiceModel>(task);

            return taskServiceModel;
        }

        public async Task RemoveAsync(Guid id)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>()
                .GetByIdAsync(id);
            TaskServiceModel taskServiceModel = _mapper
                .Map<DAL.Models.Entities.Task, TaskServiceModel>(task);

            await RemoveAsync(taskServiceModel);
        }

        public async Task RemoveAsync(TaskServiceModel entity)
        {
            DAL.Models.Entities.Task task = _mapper
                .Map<TaskServiceModel, DAL.Models.Entities.Task>(entity);
            _uow.GetRepository<DAL.Models.Entities.Task>().Remove(task);

            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskEditionServiceModel entity)
        {
            DAL.Models.Entities.Task task = _mapper
                .Map<TaskEditionServiceModel, DAL.Models.Entities.Task>(entity);
            _uow.GetRepository<DAL.Models.Entities.Task>().Update(task);

            await _uow.SaveChangesAsync();
        }
    }
}