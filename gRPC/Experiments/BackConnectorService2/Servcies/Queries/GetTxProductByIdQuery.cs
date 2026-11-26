using Common.Services.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.Queries
{
    public class GetTxProductByIdQuery : IRequestWrapper<GetProductByIdResponse>
    {
        [Required]
        public int ProductId { get; set; }
    }
}
