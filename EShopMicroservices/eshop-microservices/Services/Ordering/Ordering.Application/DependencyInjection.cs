using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application;

public static class DependencyInjection
    {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
        return services;
        }
    }

