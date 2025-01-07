﻿namespace Fonsion.be.Application.Common.Helpers;


    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }

       
}