using System;
/// <summary>
/// 
/// </summary>
namespace SV19T1081026.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class PostComment
    {
        ///<summary>
        ///
        ///</summary>
        public long CommentId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public DateTime CreatedTime { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string CommentContent { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsAccepted { get; set; }
        ///<summary>
        ///
        ///</summary>
        public long UserId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public long PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual UserAccount Commenter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
