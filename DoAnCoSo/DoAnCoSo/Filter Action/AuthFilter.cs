using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.Session.GetString("Username");

        // Allow access to Login and Register actions without authentication
        var action = context.ActionDescriptor.RouteValues["action"];
        var controller = context.ActionDescriptor.RouteValues["controller"];

        if (controller == "Taikhoan" && (action == "Login" || action == "Register"))
        {
            return;
        }

        if (string.IsNullOrEmpty(user))
        {
            context.Result = new RedirectToActionResult("Login", "Taikhoan", new { message = "Mời bạn vui lòng đăng nhập" });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
