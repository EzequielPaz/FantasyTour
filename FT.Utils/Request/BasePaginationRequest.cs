using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Utilities.Request
{
    public class BasePaginationRequest
    {
        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        public int PageSize { get; set; } = 10;
        private readonly int MaxRecords = 50;
        private string _order = "asc";
        public string Order
        {
            get => _order;
            set
            {
                if (value.ToLower() == "asc" || value.ToLower() == "desc")
                    _order = value.ToLower();
            }
        }

        public string? Sort { get; set; } = null;

        public int Records
        {
            get => PageSize;
            set => PageSize = value > MaxRecords ? MaxRecords : value;
        }
    }
}
