using HashGenerator.Api.Features.Hash.Command;
using HashGenerator.Api.Features.Hash.Query;
using MediatR;

namespace HashGenerator.Api.Features.Hash;

public static class HashEndpoints
{
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/hash", async (IMediator _mediator) =>
        {
            return await _mediator.Send(new GenerateCommand());

        }).WithTags("hash-controller");

        app.MapGet("/hash/group", async (IMediator _mediator) =>
        {
            var hashes =  await _mediator.Send(new GetGroupedHashQuery());

            return Results.Ok(hashes);

        }).WithTags("hash-controller");

        app.MapGet("/hash/all", async (IMediator _mediator) =>
        {
            var hashes = await _mediator.Send(new GetAllHashQuery());

            return Results.Ok(hashes);

        }).WithTags("hash-controller");
    }
}

