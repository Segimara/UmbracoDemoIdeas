using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace UmbracoDemoIdeas.Core.Features.Common.Controllers;
public abstract class BasePageController : RenderController
{
    public BasePageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }
    public ViewResult View<T>(T model) where T : IPublishedContent
    {
        return base.View(ViewPath(model.ContentType.Alias), model);
    }

    public ViewResult View<T>(string viewName, T model)
    {
        return base.View(viewName, model);
    }

    protected string ViewPath(string viewName)
    {
        return $"~/Views/Pages/{viewName.ToFirstUpper()}.cshtml";
    }
}
