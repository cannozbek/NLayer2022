using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService ProductService)
        {
            _mapper = mapper;
            _service = ProductService;  
        }



        //GET api/products/GetProductsWithCategory
        [HttpGet("[action]")] //otomatik olarak aşağıdaki methodun adını alıyor action
        public async Task<IActionResult> GetProductsWithCategory()
        { 
            return CreateActionResult(await _service.GetProductsWithCategory());
                
        }


        //GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productDto = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDto));
            //Burada hem Ok hem 200 gönderiyoruz yani bu biraz kötü bir kodlama sistemi bu yüzden gidip
            //Bir tane base controller yazıyoruz
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productDto));
        }

        //GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
            //Burada 201 döndük 200 yerine, 201 created anlamına geliyor
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
             await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));

            return CreateActionResult(CustomResponseDto<CustomNoContentResponseDto>.Success(204));
        }

        //Delete api/products/5
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<CustomNoContentResponseDto>.Success(204));
        }







    }
}
