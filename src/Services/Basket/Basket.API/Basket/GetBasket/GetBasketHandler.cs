
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName):IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler(IBasketRepository repository)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {


        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //get basket from database
            //var basket= await _repository.GetBasket(request.UserName);

            var basket = await repository.GetBasket(query.userName);


            return new GetBasketResult(basket);
        }
    }
}
