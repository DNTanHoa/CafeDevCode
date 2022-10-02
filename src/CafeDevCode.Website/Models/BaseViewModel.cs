using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeDevCode.Website.Models
{
    public class BaseViewModel
    {
        public string? UserName { get; set; }
        public string? IpAddress { get; set; }
        public string? RequestId { get; set; }

        public void SetBaseFromContext(HttpContext context)
        {
            this.IpAddress = context.Connection?.RemoteIpAddress?.ToString();
            this.UserName = context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            this.RequestId = context.Connection?.Id;
        }
    }
}
