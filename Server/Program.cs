var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); 

app.UseCors(builder => 
{
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.MapControllers(); 

app.Run("http://localhost:5050");
