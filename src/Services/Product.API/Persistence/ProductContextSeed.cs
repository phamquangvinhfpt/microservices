using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
    {
        if(!productContext.Products.Any())
        {
            productContext.AddRange(GetCatalogProducts());
            await productContext.SaveChangesAsync();
            logger.Information("Seed database associated with context {DbContextName}", typeof(ProductContext).Name);
        }
    }

    private static IEnumerable<CatalogProduct> GetCatalogProducts()
    {
        return new List<CatalogProduct>
        {
            new()
            {
                No = "Lotus",
                Name = "Esprit",
                Summary = "The Lotus Esprit is a sports car that was built by Lotus Cars at their Hethel factory in the United Kingdom between 1976 and 2004.",
                Description = "The silver Italdesign concept that eventually became the Esprit was unveiled at the Turin Motor Show in 1972, and was a development of a stretched Lotus Europa chassis. It was among the first of designer Giorgetto Giugiaro's polygonal 'folded paper' designs.",
                Price = (decimal) 177940.49
            },
            new()
            {
                No = "Cadillac",
                Name = "CTS",
                Summary = "The Cadillac CTS is a mid-size luxury car / executive car designed, engineered, manufactured and marketed by General Motors, and now in its third generation.",
                Description = "Initially available only as a 4-door sedan on the GM Sigma platform, GM had offered the second generation CTS in three body styles: 4-door sedan, 2-door coupe, and 5-door sport wagon also using the Sigma platform — and the third generation was offered only as a sedan, using a stretched version of the GM Alpha platform.",
                Price = (decimal) 46400.00
            }
        };
    }
}
