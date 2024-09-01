using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Todo.DAL;
using Todo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.RegisterDbDependencies();
builder.Services.RegisterRepositories();
builder.Services.RegisterTodoServices();

// Use Gzip compression
builder.Services.Configure<GzipCompressionProviderOptions>
    (options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();
