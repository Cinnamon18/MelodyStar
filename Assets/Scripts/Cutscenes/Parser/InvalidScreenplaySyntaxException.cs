using System;

namespace Cutscene
{
	[Serializable]
	class InvalidScreenplaySyntaxException : Exception
	{
		public InvalidScreenplaySyntaxException()
		{

		}

		public InvalidScreenplaySyntaxException(string message)
			: base(String.Format("Invalid Syntax for Screenplay (check your writing file): {0}", message))
		{

		}

	}
}

