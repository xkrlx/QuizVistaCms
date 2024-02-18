using Microsoft.Extensions.DependencyInjection;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class ServicesDI
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAttemptService, AttemptService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

    }
}
