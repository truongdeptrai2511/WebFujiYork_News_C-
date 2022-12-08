using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV19T1081026.DomainModels;
namespace SV19T1081026.AdminTool.Models
{
    public class CommentSearchOutput : PaginationSearchOutput
    {
        public List<PostComment> Data { get; set; }
    }
}