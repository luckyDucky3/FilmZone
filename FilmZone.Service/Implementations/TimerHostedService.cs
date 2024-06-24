using FilmZone.Domain.Models;
using FilmZone.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Service.Implementations
{
    public class TimerHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public TimerHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task StartAsync(User user)
        {
            _timer = new Timer(DeleteOrConfirmUser, user, TimeSpan.FromMinutes(30), Timeout.InfiniteTimeSpan);
            return Task.CompletedTask;
        }

        private async void DeleteOrConfirmUser(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = (User)state;
                var response = await userService.GetUserById(user.Id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if (response.Data.EmailConfirmation == true)
                    {
                        await Console.Out.WriteLineAsync($"Пользователь {user.Login} | Успешная регистрация");
                    }
                    else
                    {
                        await userService.DeleteUser(user.Id);
                    }
                }
                else if (response.StatusCode == Domain.Enum.StatusCode.UserNotFound)
                {
                    Console.WriteLine("Ошибка регистрации пользователя");
                }
                else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
                {
                    Console.WriteLine("Ошибка сервера");
                }
                else
                {
                    Console.WriteLine("Другая ошибка");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}