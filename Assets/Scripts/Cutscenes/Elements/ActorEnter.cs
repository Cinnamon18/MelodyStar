using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * Actor does something. For example, entering and exiting the scene
	 */
	public class ActorEnter : CutsceneElement {
		public GameObject actorPrefab { get; }
		public CutscenePosition position { get; }
		private string actorName;
		private static Dictionary <CutscenePosition, Vector2> positions = new Dictionary<CutscenePosition, Vector2> {
			{CutscenePosition.Left, new Vector2(-450, 475)},
			{CutscenePosition.Center, new Vector2(0, 475)},
			{CutscenePosition.Right, new Vector2(450, 475)}
		};

		public ActorEnter(string actorName, GameObject actorPrefab, CutscenePosition position) {
			this.actorPrefab = actorPrefab;
			this.position = position;
			this.actorName = actorName;
		}

		public override IEnumerator doAction(CutsceneManager cutsceneManager) {

			GameObject actorGO = Object.Instantiate(actorPrefab, cutsceneManager.cutsceneVisuals.canvas.transform);
			

			Actor actor = actorGO.GetComponent<Actor>();
			actor.position = position;
			actorGO.GetComponent<RectTransform>().anchoredPosition = ActorEnter.positions[actor.position];
			actorGO.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
			actorGO.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
			actorGO.transform.SetSiblingIndex(2);
			cutsceneManager.actors.Add(actor.actorName, actor);

			yield return cutsceneManager.cutsceneVisuals.addActor(actor, position);
			yield return cutsceneManager.dialogManager.addActor(actor);
			yield return actor.fadeIn();
		}

		public override string ToString() {
			return "Actor Enter: (Cutscene Object)" + "\tName of actor: " + actorName + " \t Side of actor: " + position;
		}
	}
}
