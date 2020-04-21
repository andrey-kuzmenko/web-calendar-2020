using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Validation
{
    public class TaskCreationValidator : AbstractValidator<TaskCreationModel>
    {
    }
}
