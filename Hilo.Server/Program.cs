using HiLo.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);


var corsPolicyName = "HiLoSignalR";
builder.Services.AddCors(options =>
{
    var policyOrigins = builder.Configuration.GetSection($"CorsOrigins:{corsPolicyName}").Get<string[]>();
    if (policyOrigins != null && policyOrigins.Length > 0)
    {
        options.AddPolicy(name: corsPolicyName,
                          policy =>
                          {
                              policy.WithOrigins(policyOrigins);
                              policy.AllowCredentials();
                              policy.AllowAnyHeader();
                          });
    }
});

builder.Services.AddSignalR();


var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseAuthorization();
app.UseCors(corsPolicyName);
app.MapHub<GameHub>("/gamehub");
app.Run();

