using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Net6Demo.Models;

public record SearchFilter(DateOnly Date, string Title)
{
    public static bool TryParse(string input, out SearchFilter filter)
    {
        filter = default;
        var splitArray = input.Split('.', '-');
        if (splitArray.Length != 4) return false;

        if (!int.TryParse(splitArray[0], out var day)) return false;
        if (!int.TryParse(splitArray[1], out var month)) return false;
        if (!int.TryParse(splitArray[2], out var year)) return false;

        filter = new (new DateOnly(year, month, day), splitArray[3]);
        return true;
    }

    public static async ValueTask<SearchFilter?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var input = context.GetRouteValue(parameter.Name!) as string ?? String.Empty;
        TryParse(input, out var filter);
        return filter;
    }
}

