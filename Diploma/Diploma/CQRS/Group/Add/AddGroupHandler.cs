using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.Views;
using EFCoreConfiguration.Repositories;
using LocaleData;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Diploma.CQRS.Group.Add
{
    public class AddGroupHandler : IRequestHandler<AddGroupRequest, ResultView>
    {
        private readonly GroupRepository _repository;
        private readonly IStringLocalizer<Messages> _localization;
        private readonly IMapper _mapper;

        public AddGroupHandler(
            GroupRepository repository,
            IMapper mapper,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _localization = localization;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(AddGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.AddGroup(_mapper.Map<EFCoreConfiguration.Models.Group>(request));
                return new();
            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
