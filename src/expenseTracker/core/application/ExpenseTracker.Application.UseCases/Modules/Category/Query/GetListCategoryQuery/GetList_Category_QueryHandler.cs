using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery
{
	public class GetList_Category_QueryHandler : UseCaseHandler<GetList_Category_QueryDto, GetList_Category_ResponseDto>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		public GetList_Category_QueryHandler(IMapper mapper, ICategoryRepository categoryRepository) : base(mapper)
		{
			_categoryRepository = categoryRepository;
		}

		#endregion

		public override async Task<GetList_Category_ResponseDto> Handle(GetList_Category_QueryDto query, CancellationToken cancellationToken)
		{
			Expression<Func<t_category, bool>> predicate = x => x.culture == query.Culture;

			List<t_category> records = await _categoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.is_other).ThenBy(n => n.name),
				predicate: predicate
				);

			return new GetList_Category_ResponseDto
			{
				Records = ListHelper.MapListRecord<t_category, GetList_Category_SingleResponseDto>(_mapper, records)
			};
		}

	}
}
