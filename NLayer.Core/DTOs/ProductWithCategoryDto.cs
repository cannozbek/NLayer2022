using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : ProductDto
    {
        public CategoryDto Category { get; set; }  
        //Repositoryler entity return ederken servicelerde direkt olarak apinin beklediği sonucu otomatik olarak dönüyor
    }
}
