using System;
using System.Collections.Generic;
using SV19T1081026.DomainModels;
using SV19T1081026.BusinessLayers;

namespace SV19T1081026.NewsPayper.Models
{
    public class PostCategoryOutput
    {
        public List<PostCategory> Data
        {
            get
            {
                return ContentService.ListCategories();
            }
        }

    }
}