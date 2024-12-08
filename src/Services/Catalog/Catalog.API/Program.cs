using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All; // Automatically create schema objects if needed
}).UseLightweightSessions();

// Configure Mapster
TypeAdapterConfig<CreateProductResult, CreateProductResponse>
    .NewConfig()
    .Map(dest => dest.Id, src => src.id); // Map Id explicitly

TypeAdapterConfig<CreateProductRequest, CreateProductCommand>
    .NewConfig()
    //.Map(dest => dest.Id, src => Guid.NewGuid())       // Map Id with a new GUID (if applicable)
    .Map(dest => dest.name, src => src.name)           // Map Name
    .Map(dest => dest.category, src => src.category)   // Map Category
    .Map(dest => dest.description, src => src.description) // Map Description
    .Map(dest => dest.imageFile, src => src.imageFile) // Map ImageFile
    .Map(dest => dest.price, src => src.price);        // Map Price


TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.Run();
