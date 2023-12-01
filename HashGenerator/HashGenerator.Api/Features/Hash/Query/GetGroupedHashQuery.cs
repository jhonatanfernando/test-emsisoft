using HashGenerator.Core.Dtos;
using HashGenerator.Core.Services;
using MediatR;

namespace HashGenerator.Api.Features.Hash.Query;

public class GetGroupedHashQuery : IRequest<IEnumerable<GroupHashDto>>
{
    public class GetGroupedHashQueryHandler : IRequestHandler<GetGroupedHashQuery, IEnumerable<GroupHashDto>>
    {
        private readonly IHashService _hashService;

        public GetGroupedHashQueryHandler(IHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task<IEnumerable<GroupHashDto>> Handle(GetGroupedHashQuery query, CancellationToken cancellationToken)
        {
            return await _hashService.GetAllGroupedAsync(cancellationToken);
        }
    }
}
