using AutoMapper;
using Diploma.Views;
using EFCoreConfiguration.Repositories;
using LocaleData;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Diploma.CQRS.Group.Get
{
    public class GetGroupHandler : IRequestHandler<GetGroupRequest, ResultView>
    {
        private readonly GroupRepository _repository;
        private readonly IStringLocalizer<Messages> _localization;
        private readonly IMapper _mapper;
        public GetGroupHandler(
                    GroupRepository repository,
                    IMapper mapper,
                    IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _localization = localization;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(GetGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetGroupsAsync(request.NameFilter);
                return new() { Views = new List<IView>(result.Select(g => _mapper.Map<GroupView>(g))) };
            }catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
