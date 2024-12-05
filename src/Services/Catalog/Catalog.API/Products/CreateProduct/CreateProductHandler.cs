using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string name, List<string> category, string description, string imageFile, decimal price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //Business logic to create a product

            //create Product entity from command object
            //save to database
            //return createproductresult result

            var product = new Product
            {
                Name = command.name,
                Category = command.category,
                Description = command.description,
                ImageFile = command.imageFile,
                Price = command.price
            };



            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
