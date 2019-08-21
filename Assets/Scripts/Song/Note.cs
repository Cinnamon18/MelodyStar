namespace Song {
	public class Note {
		public const int noteLetterCount = 12;

		public NoteLetter letter;
		public int key;

		public Note(NoteLetter letter, int key) {
			this.letter = letter;
			this.key = key;
		}

		public int toIndex() {
			return (int)(this.letter) + key * (noteLetterCount - 1);
		}

		public Note noteFromIndex(int index) {
			if (index < 0) {
				return null;
			}

			return new Note((NoteLetter)(index % noteLetterCount), (index / noteLetterCount) + 1);
		}


	}
}