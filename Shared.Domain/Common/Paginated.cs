﻿namespace Shared.Domain.Common
{
    public class Paginated<TEntity>
    {
        public List<TEntity>? Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
