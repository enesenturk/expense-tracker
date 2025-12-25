using AutoMapper;
using MediatR;

namespace ExpenseTracker.Application.Utilities.Mediator
{
	public abstract class UseCaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{

		public readonly IMapper _mapper;

		protected UseCaseHandler(IMapper mapper)
		{
			_mapper = mapper;
		}

		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

	}
}
