namespace WebApplication1.Models
{
    /// <summary>
    /// Standard API response wrapper
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Whether the request was successful
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; } = string.Empty;
        
        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; set; }
        
        /// <summary>
        /// Error details if any
        /// </summary>
        public object? Errors { get; set; }
        
        /// <summary>
        /// Response timestamp
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Create successful response
        /// </summary>
        public static ApiResponse<T> SuccessResult(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Create error response
        /// </summary>
        public static ApiResponse<T> ErrorResult(string message, object? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// Paginated response wrapper
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    public class PagedResponse<T> : ApiResponse<T>
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Whether there is a next page
        /// </summary>
        public bool HasNextPage { get; set; }
        
        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Create paginated response
        /// </summary>
        public static PagedResponse<IEnumerable<TData>> Create<TData>(
            IEnumerable<TData> data, 
            int pageNumber, 
            int pageSize, 
            int totalRecords)
        {
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            
            return new PagedResponse<IEnumerable<TData>>
            {
                Success = true,
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber > 1
            };
        }
    }

    /// <summary>
    /// Pagination parameters
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Page number (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaginationParameters()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        public PaginationParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize; // Limit max page size
        }
    }

    /// <summary>
    /// MenuCourse query parameters
    /// </summary>
   public class MenuCourseQueryParameters : PaginationParameters
    {
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }  
        public decimal? MaxPrice { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortDirection { get; set; } = "asc";
    }
}