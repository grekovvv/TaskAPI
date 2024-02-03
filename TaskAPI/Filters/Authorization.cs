using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskAPI.Filters
{
    public class Authorization : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetService<AuthorizationFilter>();
            return filter!;
        }
    }
}
