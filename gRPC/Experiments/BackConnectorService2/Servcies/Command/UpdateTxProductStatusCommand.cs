using Common.Services.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Servcies.Command
{
    public class UpdateTxProductStatusCommand : IRequestWrapper<int>
    {
        [Required(ErrorMessage = "ProductId required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Status required")]
        [Range(0, 1, ErrorMessage = "Invalid status")]
        public int Status { get; set; }
    }
}
