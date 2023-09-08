using System;

namespace DataDevelop.Scripting
{
	public class ScriptSyntaxException : Exception
	{
		internal ScriptSyntaxException(Microsoft.Scripting.SyntaxErrorException error)
			: base(error.Message, error)
		{
			ErrorCode = error.ErrorCode;
			Line = error.Line;
			Column = error.Column;
		}

		internal ScriptSyntaxException(Esprima.ParserException error)
			: base(error.Message, error)
		{
			Line = error.LineNumber;
			Column = error.Column;
		}

		public int? ErrorCode { get; set; }
		public int Line { get; set; }
		public int Column { get; set; }
	}
}
