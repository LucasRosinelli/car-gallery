using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Threading.Tasks;

namespace CarGallery.Binders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (decimal.TryParse(valueResult.FirstValue, NumberStyles.Number, CultureInfo.CurrentCulture, out var number))
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);
                bindingContext.Result = ModelBindingResult.Success(number);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return Task.CompletedTask;
        }
    }
}