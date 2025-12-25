using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<t_category, GetList_Category_SingleResponseDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

		}
	}
}
