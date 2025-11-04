using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoProject.Services;
using UmbracoProject.ViewModels;

namespace UmbracoProject.Controllers;

public class FormController(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider,
    FormSubmissonsService formSubmissonsService)
    : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{

    private readonly FormSubmissonsService _formSubmissonsService = formSubmissonsService;

    public IActionResult HandleCallbackForm(CallbackFormViewModel model)
    {
        if(!ModelState.IsValid)
            return CurrentUmbracoPage();

        var result = _formSubmissonsService.SaveCallbackRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }
        else
        {
            TempData["SuccessMessage"] = "Your message has been sent. We will get back to you as soon as possible.";
        }

        return RedirectToCurrentUmbracoPage();
    }

    public IActionResult HandleContactForm(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();
        var result = _formSubmissonsService.SaveContactRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }
        else
        {
            TempData["SuccessMessage"] = "Your message has been sent. We will get back to you as soon as possible.";
        }
        return RedirectToCurrentUmbracoPage();
    }
}

