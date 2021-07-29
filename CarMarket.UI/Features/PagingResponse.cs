using CarMarket.Core.RequestFeatures;
using System.Collections.Generic;

namespace CarMarket.UI.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
