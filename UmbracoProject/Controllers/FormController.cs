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
    FormSubmissonsService formSubmissonsService,
    IEmailService emailService
) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{

    private readonly FormSubmissonsService _formSubmissonsService = formSubmissonsService;
    private readonly IEmailService _emailService = emailService;

    public async Task<IActionResult> HandleCallbackForm(CallbackFormViewModel model)
    {
        if(!ModelState.IsValid)
            return CurrentUmbracoPage();

        var result = _formSubmissonsService.SaveCallbackRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }
        var sentToUser = await _emailService.SendOkMailToUser(model.Email, model.Name, model.SelectedOption);
        if (!sentToUser)
        {
            TempData["ErrorMessage"] = "An error occurred while sending the confirmation email. Please try again later.";
        }
        else
        {
            TempData["SuccessMessage"] = "Your message has been sent. We will get back to you as soon as possible.";
        }

        return RedirectToCurrentUmbracoPage();
    }

    public async Task<IActionResult> HandleContactForm(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();
        var result = _formSubmissonsService.SaveContactRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }
        var sentToUser = await _emailService.SendOkMailToUser(model.Email, model.Name, model.SelectedOption);
        if (!sentToUser)
        {
            TempData["ErrorMessage"] = "An error occurred while sending the confirmation email. Please try again later.";
        }
        else
        {
            TempData["SuccessMessage"] = "Your message has been sent. We will get back to you as soon as possible.";
        }
        return RedirectToCurrentUmbracoPage();
    }

    public async Task<IActionResult> HandleQuestionForm(QuestionFormViewModel model) 
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();

        var result = _formSubmissonsService.SaveQuestionFormRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }
        var sentToUser = await _emailService.QuestionFormOkMailToUser(model.Email, model.Name, model.Message);
        if (!sentToUser)
        {
            TempData["ErrorMessage"] = "An error occurred while sending the confirmation email. Please try again later.";
        }
        else
        {
            TempData["SuccessMessage"] = "Your message has been sent. We will get back to you as soon as possible.";
        }
        return RedirectToCurrentUmbracoPage();
    }

    public async Task<IActionResult> HandleContactCard(ContactCardViewModel model) 
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();
        var result = _formSubmissonsService.SaveContactCardEmailAddressRequest(model);
        if (!result)
        {
            TempData["ErrorMessage"] = "Email address received";
            return RedirectToCurrentUmbracoPage();
        }
        var sentToUser = await _emailService.ContactCardOkMailToUser(model.Email);
        if (!sentToUser)
        {
            TempData["ErrorMessage"] = "Email address could not be retrieved";
        }
        else
        {
            TempData["SuccessMessage"] = "Email address could not be retrieved";
        }
        return RedirectToCurrentUmbracoPage();
    }
}

