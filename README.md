# RequestResponseLogging

Logged request/response in web API
This is a logged service implemented in ASP.NET Core. It allows you to log any request and response.

## Features

- Logged any request with a message and error code
- Logged any response for tracking
- Logged unhandled exceptions
- Middleware for easy use for any applications

## Technologies Used

- ASP.NET Core
- C#

## Clone the repository:
git clone https://github.com/nikravesh/RequestResponseLogging.git

## Give a Star! ⭐
If you find this project helpful or interesting, please consider giving it a star on GitHub. It helps to support the project and gives recognition to the contributors.


## Getting Started
To get started with the Request/Response and unhandled logging just use the middleware in your project

### Usage
Very simple you can use these middlewares in the blow you can see the example : 
```
using RequestResponseLogging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseUnHandleException();
app.UserRequestResponseLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```
Pay attention, UseUnHandleException must be on top of it can log unhandled exceptions when the project has been launched.

## License
This project is licensed under the MIT License: [MIT License](https://opensource.org/licenses/MIT).

## Stay Connected
Feel free to raise any questions or suggestions through GitHub issues.

