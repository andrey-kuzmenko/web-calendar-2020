using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCalendar.Common;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.EF;
using WebCalendar.DAL.EF.Context;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.DAL.Repositories.Contracts;
using WebCalendar.DAL.Repositories.Implementation;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.EmailSender;
using WebCalendar.Services.EmailSender.Contracts;
using WebCalendar.Services.EmailSender.Implementation;
using WebCalendar.Services.Implementation;
using WebCalendar.Services.PushNotification.Contracts;
using WebCalendar.Services.PushNotification.Implementation;
using WebPush;
using VapidDetails = WebCalendar.Services.PushNotification.VapidDetails;

namespace WebCalendar.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            
            string connection = configuration["ConnectionString"];

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection,
                    builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); }));

            services.AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireUppercase = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IDataInitializer, EFDataInitializer>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepositoryAsync<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IMapper, WebCalendarAutoMapper>();

            services.AddScoped<WebPushClient>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            
            var vapidDetails = configuration
                .GetSection("VapidDetails")
                .Get<VapidDetails>();

            services.AddSingleton(vapidDetails);

            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}