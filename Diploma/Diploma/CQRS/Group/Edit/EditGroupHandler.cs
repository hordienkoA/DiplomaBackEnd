using AutoMapper;
using Diploma.Views;
using EFCoreConfiguration.Repositories;
using LocaleData;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Diploma.CQRS.Group.Edit
{
    public class EditGroupHandler : IRequestHandler<EditGroupRequest, ResultView>
    {
        private readonly GroupRepository _repository;
        private readonly IStringLocalizer<Messages> _localization;
        private readonly IMapper _mapper;

        public EditGroupHandler(
            GroupRepository repository,
            IMapper mapper,
            IStringLocalizer<Messages> localizer)
        {
            _repository = repository;
            _localization = localizer;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(EditGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.EditGroup(_mapper.Map<EFCoreConfiguration.Models.Group>(request));
                return new() { Views = new List<IView> { _mapper.Map<GroupView>(result) }};
            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
