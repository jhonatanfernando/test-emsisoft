using HashGenerator.Core.Dtos;
using HashGenerator.Core.Services;
using MediatR;

namespace HashGenerator.Api.Features.Hash.Query;

public class GetAllHashQuery : IRequest<IEnumerable<HashDto>>
{
    public class GetAllHashQueryHandler : IRequestHandler<GetAllHashQuery, IEnumerable<HashDto>>
    {
        private readonly IHashService _hashService;

        public GetAllHashQueryHandler(IHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task<IEnumerable<HashDto>> Handle(GetAllHashQuery query, CancellationToken cancellationToken)
        {
            return await _hashService.GetAllAsync(cancellationToken);
        }
    }
}
