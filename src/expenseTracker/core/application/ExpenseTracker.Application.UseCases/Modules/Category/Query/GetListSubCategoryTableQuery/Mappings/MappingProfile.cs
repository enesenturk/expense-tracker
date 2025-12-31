using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<t_sub_category, GetList_SubCategoryTable_SingleResponseDto>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.t_category_id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
				.ForMember(dest => dest.IsExpenseCreated, opt => opt.MapFrom(src => src.is_expense_created))
				.ForMember(dest => dest.IsOther, opt => opt.MapFrom(src => src.is_other));

		}
	}
}
