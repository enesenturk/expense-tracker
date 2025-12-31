using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<t_category, GetList_CategoryTable_SingleResponseDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
				.ForMember(dest => dest.Culture, opt => opt.MapFrom(src => src.culture))
				.ForMember(dest => dest.IsExpenseCreated, opt => opt.MapFrom(src => src.is_expense_created))
				.ForMember(dest => dest.IsOther, opt => opt.MapFrom(src => src.is_other));

		}
	}
}
