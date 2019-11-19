using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cutscene;

namespace Cutscene.Elements {
	/*
	 * Can represent a line delivery, scene transition, 
	 * essentially any script component that translates to something on screen
	 */
	public abstract class CutsceneElement {

		// do whatever you're supposed to do in the context of a cutscene! sorry, not a great method name.
		public abstract IEnumerator doAction(
			CutsceneManager cutsceneManager
		);
	}
}
