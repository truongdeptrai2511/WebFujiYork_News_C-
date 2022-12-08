using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081026.DomainModels
{
    public class PostComment
    {
        public long CommentId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CommentContent { get; set; }
        public bool IsAccepted { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
