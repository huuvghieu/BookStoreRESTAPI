using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.InterfaceService
{
    public interface ICategoryService
    {
        Task<BasePagingViewModel<CategoryResponse>> GetCategories(PagingRequest pagingRequest, CategoryRequest categoryRequest);
        Task<BaseResponseViewModel<CategoryResponse>> PostCategory(CategoryRequest model);
        Task<BaseResponseViewModel<CategoryResponse>> PutCategory(int id, CategoryRequest model);
        Task<BaseResponseViewModel<CategoryResponse>> DeleteCategory(int id);
        Task<BaseResponseViewModel<CategoryResponse>> GetCategoryByID(int id);
    }
}
