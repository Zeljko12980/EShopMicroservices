

using Mapster;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string name, List<string> category, string description, string imageFile, decimal price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                /*             var command = new CreateProductCommand(
                 name: request.name,
                 category: request.category,
                 description: request.description,
                 imageFile: request.imageFile,
                 price: request.price
             );*/

                var command = request.Adapt<CreateProductCommand>();


                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
