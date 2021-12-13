using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.LessonInfo
{
    public class GetLessonInfoHandler : IRequestHandler<GetLessonInfoRequest, ResultView>
    {
        private readonly LessonInfoRepository _repository;
        private readonly LessonRepository _lessonRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;
        public GetLessonInfoHandler(
            LessonInfoRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper,
            LessonRepository lessonRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _mapper = mapper;
            _lessonRepository = lessonRepository;
        }
        public async Task<ResultView> Handle(GetLessonInfoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var lesson = await _lessonRepository.GetLessonAsync(request.LessonId.Value, user);
                var lessonInfo = await _repository.GetLessonInfoAsync(request.LessonId, user.UserName);
                if (lessonInfo == null && lesson != null)
                {
                    lessonInfo = new EFCoreConfiguration.Models.LessonInfo
                    {
                        LessonId = request.LessonId.Value,
                        Status = EFCoreConfiguration.Models.Enums.StatusEnum.Open,
                        ValidTill = DateTime.Now.AddYears(1),
                        Attemts = 3,
                        IsPassed = false,
                        UserId = user.Id
                    };
                    _repository.AddLessonInfo(lessonInfo);
                    
                }
                return new()
                {
                    Views = new List<IView> { _mapper.Map<LessonInfoView>(lessonInfo) }
                };

            }
            catch (Exception ex)
            {
                return new ResultView() { Error = new(400, ex.Message) };
            }
        }
    }
}
