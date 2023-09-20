using AutoMapper;
using Domains.Entities;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels.ProductDTO;

namespace Services.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IProductRepository productRepository, IMapper mapper)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}
		public ProductDTO CreateProduct(ProductCreateDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);

			_productRepository.Insert(product);
			if (_productRepository.SaveChanges())
			{
				return _mapper.Map<ProductDTO>(_productRepository.Find(x => x.Id == product.Id, x => x.Category).First());
			}
			else
			{
				throw new Exception("Save changes failed!");
			}
		}

		public bool DeleteProduct(Guid id)
		{
			var product = _productRepository.Find(x => x.Id == id).First();
			if (product != null)
			{
				_productRepository.Delete(product);
				return _productRepository.SaveChanges() ?
					true : throw new Exception("Save changes failed!");
			}
			else
			{
				throw new Exception("Not found!");
			}
		}

		public ProductDTO GetProductById(Guid id)
		{
			return _mapper.Map<ProductDTO>(_productRepository.Find(x => x.Id == id, x => x.Category).First());
		}

		public IEnumerable<ProductDTO> GetProducts() => _mapper.Map<IEnumerable<ProductDTO>>(_productRepository.GetAll(x => x.Category));

		public IEnumerable<ProductDTO> Search(string search)
		{
			if (int.TryParse(search, out int result))
			{
				return _mapper.Map<IEnumerable<ProductDTO>>(_productRepository
					.Find(x => x.Name.ToLower().Contains(search.ToLower())
					|| x.UnitPrice <= result
					|| x.UnitPrice <= result, x => x.Category));
			}
			else
			{
				return _mapper.Map<IEnumerable<ProductDTO>>(_productRepository.Find(x => x.Name.ToLower().Contains(search.ToLower()), x => x.Category));
			}
		}

		public ProductDTO UpdateProduct(ProductUpdateDTO product)
		{
			_productRepository.Update(_mapper.Map<Product>(product));
			return _productRepository.SaveChanges()
				? _mapper.Map<ProductDTO>(_productRepository.Find(x => x.Id == product.Id, x => x.Category).First())
				: throw new Exception("Save changes failed!");
		}


	}
}
