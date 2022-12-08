using SV19T1081026.DomainModels;
using System.Collections.Generic;
using SV19T1081026.BusinessLayers;
namespace News.Models
{
    public class PostCategoryOutput
    {
        public List<PostCategory> Data { 
            get
            {
                return ContentService.ListCategories();
            }
        }

    }
}