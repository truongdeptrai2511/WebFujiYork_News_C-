

namespace SV19T1081026.DomainModels
{
    /// <summary>
    /// Phân loại bài viết
    /// </summary>
    public class PostCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrlName { get; set; }
        public string CategoryDescriptions { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
    }
}
