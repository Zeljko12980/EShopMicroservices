﻿using FluentValidation;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);
   
    public class DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Product ID is required");
        }
    }

    public class DeleteProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
