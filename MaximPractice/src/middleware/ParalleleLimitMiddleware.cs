using MaximPractice.Services;
using MaximPractice.src.Settings;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace MaximPractice.src.middleware
{
    public class ParalleleLimitMiddleware
    {
        private readonly RequestDelegate next;
        public ParalleleLimitMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, UsersCounterService usersCounter, AppSettings appSettings)
        {
            if (usersCounter.Count >= appSettings.Settings.ParallelLimit)
            {
                context.Response.StatusCode = 503;
            }
            else
            {
                usersCounter.AddUserCount();
                await next.Invoke(context);
                usersCounter.RemoveUserCount();
            }
        }
    }
}
