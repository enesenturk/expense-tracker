using AutoMapper;
using Base.Dto;

namespace ExpenseTracker.Application.Utilities.Helpers
{
	public class ListHelper
	{

		public static List<Destination> MapListRecord<Source, Destination>(IMapper mapper, List<Source> sources) where Destination : ListRecordDto
		{
			if (sources == null)
				return null;

			List<Destination> destinations = new List<Destination>();

			for (int i = 0; i < sources.Count; i++)
			{
				Destination destination = mapper.Map<Destination>(sources[i]);

				destination.Index = i;

				destinations.Add(destination);
			}

			return destinations;
		}

	}
}
