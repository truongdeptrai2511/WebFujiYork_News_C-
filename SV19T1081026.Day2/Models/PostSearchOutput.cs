using System.Collections.Generic;
using SV19T1081026.DomainModels;

namespace SV19T1081026.AdminTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Post> Data { get; set; }
    }
}