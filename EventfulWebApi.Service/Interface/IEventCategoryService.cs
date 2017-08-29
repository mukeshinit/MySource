namespace EventfulWebApi.Service.Interface
{
    using EventfulWebApi.Models;
    using System.Collections.Generic;

    public interface IEventCategoryService
    {
        EventCategories GetEventCategory();
    }
}
