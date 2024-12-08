using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nabeghe.Web.Extensions;

public static class ModelStateExtensions
{
    public static string GetModelError(this ModelStateDictionary modelState)
    {
        return modelState.Values.FirstOrDefault()
            .Errors.FirstOrDefault().ErrorMessage ?? "خطایی یافت نشد";
    }
}