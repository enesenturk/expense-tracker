namespace Base.Exceptions.ExceptionModels
{
	public class BusinessRuleException : Exception
	{

		public BusinessRuleException()
		{

		}

		public BusinessRuleException(string message, string code = "")
			: base(message, new Exception($"{code}"))
		{

		}

	}
}
