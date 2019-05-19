using System;
using System.Collections.Generic;

namespace api.middleware.exception.handler
{
    public class ExceptionHandlerOptions
    {
		public ExceptionHandlerOptions() 
		{
			Maps = new Dictionary<Type, ExceptionHandlerInfo>();
		}

		public IDictionary<Type, ExceptionHandlerInfo> Maps { get; set; }
	}
}
