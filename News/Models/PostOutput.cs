using SV19T1081026.BusinessLayers;
using SV19T1081026.DomainModels;
using System.Collections.Generic;

namespace News.Models
{
    public class PostOutput : PaginationSearchOutput
    {
        public int CategoryId { get; set; }
        public List<Post> Data
        {
            get
            {
                return ContentService.ListPosts();
            }
        }
    }
}