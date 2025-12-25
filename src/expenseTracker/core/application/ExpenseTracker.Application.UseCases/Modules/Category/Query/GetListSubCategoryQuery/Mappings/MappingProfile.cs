using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<t_sub_category, GetList_SubCategory_SingleResponseDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

		}
	}
}
