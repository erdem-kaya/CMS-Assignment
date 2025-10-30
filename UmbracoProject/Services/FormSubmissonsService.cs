using Umbraco.Cms.Core.Services;
using UmbracoProject.ViewModels;

namespace UmbracoProject.Services;

public class FormSubmissonsService(IContentService contentService)
{
    private readonly IContentService _contentService = contentService;

    public bool SaveCallbackRequest(CallbackFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(c => c.ContentType.Alias == "fromSubmissions");
            if (container == null)
                return false;

            var requestName = $"Message: {model.Name} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            var request = _contentService.Create(requestName, container, "callbackRequest");
            if (request == null)
                return false;

            request.SetValue("callbackRequestName", model.Name);
            request.SetValue("callbackRequestEmail", model.Email);
            request.SetValue("callbackRequestPhone", model.Phone);
            request.SetValue("callbackRequestOption", model.SelectedOption);

            var saveResult = _contentService.Save(request);
            return saveResult.Success;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            return false;
        }
    }
}
