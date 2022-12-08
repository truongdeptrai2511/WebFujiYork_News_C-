using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081026.NewsPayper
{
    public abstract class PaginationSearchOutput
    {
        /// <summary>
        /// Trang được hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Tổng số dòng
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0) return 1;

                int c = RowCount / PageSize;

                if (RowCount % PageSize > 0)
                    c += 1;
                return c;
            }
        }
        public string SearchValue { get; set; }
    }
}