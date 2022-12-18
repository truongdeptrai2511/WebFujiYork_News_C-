using System.Collections.Generic;
using SV19T1081026.DomainModels;

namespace SV19T1081026.NewsPayper.Models
{
    public class PostSearchOutput : PaginationSearchOutput 
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Post DataPost { get; set; }
        public List<Post> Data { get; set; }

        public Post AllowComment { get; set; }

    }
}