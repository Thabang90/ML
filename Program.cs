using Microsoft.EntityFrameworkCore;
using QuestionExplorer.Admins.Repositories;
using QuestionExplorer.Admins.Repositories.Interfaces;
using QuestionExplorer.Context;
using QuestionExplorer.Questions.Repositories;
using QuestionExplorer.Questions.Repositories.Interfaces;
using QuestionExplorer.Users.Repositories;
using QuestionExplorer.Users.Repositories.Interfaces;
using QuestionExplorer.UtilityService;
using QuestionExplorer.UtilityService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConn"));
});
builder.Services.AddCors(option =>
{
    option.AddPolicy("myPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
