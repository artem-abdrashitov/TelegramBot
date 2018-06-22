using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TelegramBot.BackgroundTasks;
using TelegramBot.Commands;
using TelegramBot.Commands.GetSettings;
using TelegramBot.Commands.HardwareCost;
using TelegramBot.Data;
using TelegramBot.Services;

namespace TelegramBot
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddCors()
                .AddJsonFormatters();

            var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
            var domain = Environment.GetEnvironmentVariable("HEROKU_URL");
            
            var telegramBotClient = new TelegramBotClient(token);

            var isDevelopment = !string.IsNullOrEmpty(domain);

            var webhookUrl = isDevelopment ? $"{domain}/api/webhook" : string.Empty;
            telegramBotClient.SetWebhookAsync(webhookUrl);

            services.AddSingleton<ITelegramBotClient>(telegramBotClient);
            services.AddSingleton<HttpClientWrapper>();
            services.AddSingleton<DataContext>();
            services.AddSingleton<ProfitMineService>();

            services.AddSingleton(typeof(IRepository<>), typeof(MongoDbRepository<>));

            // Add scheduled tasks & scheduler
            services.AddSingleton<IScheduledTask, PoloniexTask>();
            services.AddSingleton<IScheduledTask, CoinMarketTask>();
            services.AddSingleton<IHostedService, SchedulerHostedService>();

            // Telegram commands
            services.AddScoped<ITelegramCommand, HelpCommand>();//help
            services.AddScoped<ITelegramCommand, LoginCommand>();//login

            services.AddScoped<ITelegramCommand, GetGostCommand>();//Get cost from CoinMarket
            services.AddScoped<ITelegramCommand, MainCommand>();//Get all cost from Poloniex
            services.AddScoped<ITelegramCommand, ProfitCommand>();//Get profit mine
            services.AddScoped<ITelegramCommand, S9Command>();//s9
            services.AddScoped<ITelegramCommand, D3Command>();//d3
            services.AddScoped<ITelegramCommand, L3PlusCommand>();//l3
            services.AddScoped<ITelegramCommand, B8Command>();//b8
            services.AddScoped<ITelegramCommand, Nvidia1070>();//n1070
            services.AddScoped<ITelegramCommand, Nvidia1080Ti>();//n1080ti
            services.AddScoped<ITelegramCommand, HardwareSettingsCommand>();//hw
            services.AddScoped<TelegramCommandService>();
        }

        public void Configure(IApplicationBuilder app)
        {
	        app
				.UseDefaultFiles()
				.UseStaticFiles()
				.UseMvcWithDefaultRoute()
				.UseDeveloperExceptionPage()
				.UseDatabaseErrorPage();
        }
    }
}