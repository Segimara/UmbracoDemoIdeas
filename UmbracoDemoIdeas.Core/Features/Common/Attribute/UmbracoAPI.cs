using Microsoft.AspNetCore.Mvc;

namespace UmbracoDemoIdeas.Core.Features.Common.Attribute;
/// <summary>
/// Use this attribute to customize routes in controller
/// </summary>
internal class UmbracoAPIControllerAttribute : RouteAttribute
{
    public UmbracoAPIControllerAttribute() : base("[controller]") { }
}
internal class UmbracoAPIActionAttribute : RouteAttribute
{
    public UmbracoAPIActionAttribute() : base("[action]") { }
}
