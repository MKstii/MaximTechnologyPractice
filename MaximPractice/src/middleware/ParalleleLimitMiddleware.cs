using MaximPractice.Services;
using MaximPractice.src.Settings;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace MaximPractice.src.middleware
{
    public class ParalleleLimitMiddleware
    {
        private readonly RequestDelegate next;
        private readonly UsersCounterService usersCounter;
        public ParalleleLimitMiddleware(RequestDelegate next, UsersCounterService usersCounter)
        {
            this.next = next;
            this.usersCounter = usersCounter;
        }

        public async Task InvokeAsync(HttpContext context, AppSettings appSettings)
        {
            usersCounter.AddUserCount();
            if (usersCounter.Count > appSettings.Settings.ParallelLimit)
            {
                context.Response.StatusCode = 503;
            }
            else
            {
                await next.Invoke(context);
            }
            usersCounter.RemoveUserCount();
        }
    }
}
