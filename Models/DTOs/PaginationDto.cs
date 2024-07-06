namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; } = 0;
        public int TotalPage { get; set; } = 1;
        public List<T> Data { get; set; } = [];
    }

    public enum PageSizeOption
    {
        TenPerPage = 10,
        TwentyPerPage = 20,
        FiftyPerPage = 50,
    }

    public class PaginationBuilder<T>
    {
        protected int Page;
        protected int PageSize;
        protected int TotalCount;
        protected int TotalPage;
        protected List<T> Data;

        protected PaginationBuilder()
        {
            Page = 1;
            PageSize = 10;
            TotalCount = 0;
            TotalPage = 1;
            Data = [];
        }

        public static PaginationBuilder<T> Create()
        {
            PaginationBuilder<T> builder = new();
            return builder;
        }
        
        public PaginationBuilder<T> WithCurrentPage(int page)
        {
            if(page < 1)
            {
                throw new ArgumentException($"{nameof(Page)} must be greater or equal to 1");
            }

            Page = page;

            return this;
        }

        public PaginationBuilder<T> WithPageSize(PageSizeOption pageSizeOption)
        {
            ArgumentNullException.ThrowIfNull(pageSizeOption, nameof(pageSizeOption));
            
            PageSize = (int) pageSizeOption;
            return this;
        }
        
        public PaginationBuilder<T> WithData(List<T> data)
        {
            TotalCount = data.Count;
            TotalPage = (int) Math.Ceiling((double) data.Count / PageSize);
            Data = data.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

            return this;
        }

        public PaginationBuilder<T> WithData(List<T> data, int totalCount, int totalPage)
        {
            TotalCount = totalCount;
            TotalPage = totalPage;
            Data = data;

            return this;
        }

        public Pagination<T> Build()
        {
            return new Pagination<T>()
            {
                CurrentPage = this.Page,
                Data = this.Data,
                PageSize = this.PageSize,
                TotalCount = this.TotalCount,
                TotalPage = this.TotalPage
            };
        }
    }
}
