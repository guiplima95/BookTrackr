namespace Book.API.Endpoints;

public interface IEndpoint
{
    void MapBookApiEndpoint(IEndpointRouteBuilder app);
}