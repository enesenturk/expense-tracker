using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery
{
	public class GetList_CategoryTable_QueryHandler : UseCaseHandler<GetList_CategoryTable_QueryDto, GetList_CategoryTable_ResponseDto>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		public GetList_CategoryTable_QueryHandler(IMapper mapper, ICategoryRepository categoryRepository) : base(mapper)
		{
			_categoryRepository = categoryRepository;
		}

		#endregion

		public override async Task<GetList_CategoryTable_ResponseDto> Handle(GetList_CategoryTable_QueryDto query, CancellationToken cancellationToken)
		{
			Expression<Func<t_category, bool>> predicate = x => x.culture == query.Culture;

			List<t_category> records = await _categoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.is_other).ThenBy(n => n.name),
				predicate: predicate
				);

			return new GetList_CategoryTable_ResponseDto
			{
				Records = _mapper.Map<List<GetList_CategoryTable_SingleResponseDto>>(records)
			};
		}

	}
}
