using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PortfolioToEditDto
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public int IconColor { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }
    }
}