using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarGallery.Binders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new DecimalModelBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(decimal) ? this.binder : null;
        }
    }
}