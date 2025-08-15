namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Request}", request);

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
