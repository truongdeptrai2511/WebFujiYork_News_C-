using System;
using System.Collections.Generic;
using SV19T1081026.DomainModels;

namespace SV19T1081026.NewsPayper.Models
{
    public class CommentSearchOutput : PaginationSearchOutput
    {
        public List<PostComment> Data { get; set; }
    }
}