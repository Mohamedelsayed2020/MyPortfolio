using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModel
{
    public class HomeViewModel
    {
        public Owner owner { get; set; } 
        public List<PortfolioItem>portfolioItems { get; set; }
    }
}
