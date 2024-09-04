using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoDemoIdeas.Core.Features.Common.Controllers;

namespace UmbracoDemoIdeas.Core;
public static class DependencyInjection
{
    public static IUmbracoBuilder RegisterCore(this IUmbracoBuilder builder)
    {
        builder.Services.Configure<UmbracoRenderingDefaultsOptions>(c =>
         {
             c.DefaultControllerType = typeof(DefaultPageController);
         });

        return builder;
    }
}

