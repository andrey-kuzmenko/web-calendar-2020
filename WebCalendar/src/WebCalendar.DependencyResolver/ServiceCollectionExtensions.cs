using System;
using Hangfire;
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
using WebCalendar.EmailSender;
using WebCalendar.EmailSender.Contracts;
using WebCalendar.Services.Implementation;
using WebCalendar.PushNotification;
using WebCalendar.PushNotification.Contracts;
using WebCalendar.PushNotification.Implementation;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Implementation;

namespace WebCalendar.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration["ConnectionString"];

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection,
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
            
            string hangfireConnection = configuration["HangfireConnectionString"];
            services.AddDbContext<HangfireDbContext>(options =>
                options.UseSqlServer(hangfireConnection,
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
            services.AddHangfire(x => x.UseSqlServerStorage(hangfireConnection));

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
            services.AddScoped<HangfireDbInitializer>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepositoryAsync<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IMapper, WebCalendarAutoMapper>();
            
            var firebaseNotification = configuration
                .GetSection("FirebaseNotification")
                .Get<FirebaseNotification>();
            
            services.AddSingleton<IPushNotificationSender, PushNotificationSender>(p =>
                new PushNotificationSender(firebaseNotification));
            
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            
            services.AddScoped<IEmailSender, EmailSender.Implementation.EmailSender>(e => 
                new EmailSender.Implementation.EmailSender(emailConfig));

            services.AddScoped<INotificationService, NotificationService>();

        }
    }
}