using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productEntity = await _productRepository.GetProductAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(productEntity); 
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var productEntity = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task Add(ProductDTO productDto)
        {
            var porductEntity = _mapper.Map<Product>(productDto);
            await _productRepository.CreateAsync(porductEntity);
        }

        public async Task Update(ProductDTO productDto)
        {
            var porductEntity = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(porductEntity);
        }

        public async Task Remove(int? id)
        {
            var porductEntity = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.RemoveAsync(porductEntity);
        }
    }
}
