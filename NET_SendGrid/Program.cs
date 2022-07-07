using NET_SendGrid.Models;
using NET_SendGrid.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// SendGrid
builder.Services.AddTransient<ICustomEmailSender, SendGridEmailSender>();
builder.Services.Configure<SendGridEmailSenderOptions>(options =>
{
    options.ApiKey = builder.Configuration["SendGrid:ApiKey"];
    options.SenderEmail = builder.Configuration["SendGrid:SenderEmail"];
    options.SenderName = builder.Configuration["SendGrid:SenderName"];
    options.Template = builder.Configuration.GetSection("SendGrid:Template").Get<SendGridEmailTemplateOptions>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Production.WebAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
