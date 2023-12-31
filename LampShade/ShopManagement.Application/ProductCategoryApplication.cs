﻿

using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryApp;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public OperationResult Create(CreateProductCategoryCommand command)
        {

            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);


            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords
                , command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategoryCommand command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name,command.Description,command.Picture,command.PictureAlt,
                command.PictureTitle,command.Keywords,command.MetaDescription,slug);
            _productCategoryRepository.Save();
            return operation.Succeeded();
            
        }

        public EditProductCategoryCommand GetDetailsBy(long id)
        {
            return _productCategoryRepository.GetDetailsBy(id);

        }

 
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
        public List<ProductCategoryViewModel> GetCategoryItem()
        {
            return _productCategoryRepository.GetCategoryItems();
        }

        public OperationResult IsRemove(long id)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(id);
            if (productCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productCategory.Remove();
            _productCategoryRepository.Save();

            return operation.Succeeded();

        }

        public OperationResult IsRestore(long id)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(id);
            if (productCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productCategory.Restore();
            _productCategoryRepository.Save();

            return operation.Succeeded();
        }
    }
}
