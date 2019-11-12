using System;

namespace Cutscene {
	[Serializable]
	class InvalidScreenplaySyntaxException : Exception {
		public InvalidScreenplaySyntaxException() {

		}

		public InvalidScreenplaySyntaxException(string message, string segment = "")
			: base(String.Format("Invalid Syntax for Screenplay (check your writing file): {0} {1}", message, "in segment " + segment)) {

		}

	}
}

