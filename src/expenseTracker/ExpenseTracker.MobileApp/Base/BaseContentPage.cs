using AutoMapper;
using MediatR;

namespace ExpenseTracker.MobileApp.Base
{
	public class BaseContentPage : ContentPage
	{

		protected readonly IMediator _mediator;
		protected readonly IMapper _mapper;

		protected readonly BaseMediatorCaller _baseMediatorCaller;

		public BaseContentPage(IMediator mediator, IMapper mapper, BaseMediatorCaller baseMediatorCaller)
		{
			_mediator = mediator;
			_mapper = mapper;

			_baseMediatorCaller = baseMediatorCaller;
		}

	}
}