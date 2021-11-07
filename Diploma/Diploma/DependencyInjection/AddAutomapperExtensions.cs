using AutoMapper;
using Diploma.CQRS.Lessons;
using Diploma.Views;
using EFCoreConfiguration.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    public static class AddAutomapperExtensions
    {
        public static void AddAutomapperCustom(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lesson, LessonView>();
            CreateMap<Subject, SubjectView>();
            CreateMap<Lesson, AddLessonRequest>();
            CreateMap<EditLessonRequest, Lesson>();
            CreateMap<Lesson, LessonView>();
        }
    }
}
