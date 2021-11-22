using AutoMapper;
using Diploma.CQRS.Group.Add;
using Diploma.CQRS.Group.Edit;
using Diploma.CQRS.Lessons;
using Diploma.CQRS.Subjects;
using Diploma.CQRS.Task;
using Diploma.Views;
using EFCoreConfiguration.Models;

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
            CreateMap<AddLessonRequest, Lesson>();
            CreateMap<EditLessonRequest, Lesson>();
            CreateMap<EditSubjectRequest, Subject>();
            CreateMap<Lesson, LessonView>();
            CreateMap<AddTaskRequest, EFCoreConfiguration.Models.Task>();
            CreateMap<EFCoreConfiguration.Models.Task, TaskView>();
            CreateMap<AddGroupRequest, Group>();
            CreateMap<EditGroupRequest, Group>();
            CreateMap<Group, GroupView>();
        }
    }
}
