using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV19T1081026.DomainModels;

namespace News.Models
{
    public class PostSearchOutput : PaginationSearchOutput
    {
        public int CategoryId { get; set; }

        public List<Post> Data { get; set; }

    }
}