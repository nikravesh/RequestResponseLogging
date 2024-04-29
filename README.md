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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

## Logging sample
```
2024-04-28 22:44:32.467 +03:30 INF Request: https localhost:7145/api/UserAuthentication/login  {
  "userName": "string",
  "password": "string"
}
Request headers: 

2024-04-28 22:44:33.367 +03:30 INF Response: 200: {"userName":"string","password":"string"}
```

## License
This project is licensed under the MIT License: [MIT License](https://opensource.org/licenses/MIT).

## Stay Connected
Feel free to raise any questions or suggestions through GitHub issues.

