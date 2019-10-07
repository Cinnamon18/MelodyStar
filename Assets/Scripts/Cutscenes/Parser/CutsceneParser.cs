using System.Collections.Generic;
using UnityEngine;
using Cutscene.Elements;
using System;
using System.Linq;

/*
 * Takes in the cutscene text and returns a list of cutscene objects
 * Note: If there are any syntax errors in the text, null will be returned and bad things will happen :(
 */

namespace Cutscene {

	[Serializable]
	public struct namedPrefabForEditor {
		public string name;
		public GameObject prefab;
	}

	public class CutsceneParser : MonoBehaviour {

		public namedPrefabForEditor[] actorPrefabsEditor;
		public namedPrefabForEditor[] backgroundPrefabsEditor;
		private Dictionary<string, GameObject> actorPrefabs = new Dictionary<string, GameObject>();
		private Dictionary<string, GameObject> backgroundPrefabs = new Dictionary<string, GameObject>();

		void Awake() {
			foreach (namedPrefabForEditor actorPrefab in actorPrefabsEditor) {
				actorPrefabs.Add(actorPrefab.name.ToUpper(), actorPrefab.prefab);
			}
			foreach (namedPrefabForEditor backgroundPrefab in backgroundPrefabsEditor) {
				backgroundPrefabs.Add(backgroundPrefab.name, backgroundPrefab.prefab);
			}
		}

		public List<CutsceneElement> parse(string cutsceneText) {
			List<CutsceneElement> objects = new List<CutsceneElement>();

			//Each one of these represents an individual object (after parsing)
			string[] segments = splitText(cutsceneText);

			foreach (string segment in segments) {
				CutsceneElement newCutsceneObject;
				try {
					newCutsceneObject = BuildObject(segment);
					if (newCutsceneObject != null) {
						objects.Add(newCutsceneObject);
					}
				} catch (InvalidScreenplaySyntaxException ex) {
					Debug.LogError(ex.Message);
					//newCutsceneObject = new ActorLine("George P. Burdell", "Shocked", "Some noob messed up the script syntax.");
					//objects.Add(newCutsceneObject);
				}
			}

			return objects;
		}

		private CutsceneElement BuildObject(string segment) {
			//need to get each individual line in a segment
			string[] subSegments = segment.Split('\n');
			string header = subSegments[0];
			string[] tokens = header.Split(' ');

			if (tokens[0].Substring(0, 2).Equals("//")) {
				// Comment, exclude
				return null;
			} else if (tokens[0].Contains("Scene:")) {
				//Scene transition
				if (tokens.Length == 2) {
					string sceneName = tokens[1];
					GameObject background = null;
					try {
						background = backgroundPrefabs[sceneName];
					} catch (KeyNotFoundException) {
						throw new InvalidScreenplaySyntaxException("Unrecognized background", segment);
					}
					return new CutsceneTransition(background);
				} else {
					throw new InvalidScreenplaySyntaxException("Invalid number of tokens for Scene Transition", segment);
				}
			} else if (tokens[0].Equals("Enter")) {
				// New actor
				string side = tokens[1].Substring(0, tokens[1].Length - 1);
				CutscenePosition position = CutscenePosition.Center;
				switch (side.ToLower()) {
					case "left":
						position = CutscenePosition.Left;
						break;
					case "center":
						position = CutscenePosition.Center;
						break;
					case "right":
						position = CutscenePosition.Right;
						break;
					default:
						throw new InvalidScreenplaySyntaxException("Unrecognized entry side", segment);
				}
				string actorName = tokens[2].ToUpper();

				GameObject actorPrefab = null;
				try {
					actorPrefab = actorPrefabs[actorName];
				} catch (KeyNotFoundException) {
					throw new InvalidScreenplaySyntaxException("Unrecognized actor", segment);
				}
				return new ActorEnter(actorName, actorPrefab, position);
			} else if (tokens[0].Equals("Exit:")) {
				// actor leaving
				return new ActorExit(tokens[1].ToUpper());
			} else {
				//Actor Line
				string actorName = tokens[0];
				string pose;
				string line = "";

				if ((tokens.Length == 2) || (tokens.Length == 1)) {
					if (tokens.Length == 2) {
						//Pose is given
						if ((tokens[1].IndexOf('(') == -1) || (tokens[1].IndexOf(')') == -1)) {
							throw new InvalidScreenplaySyntaxException("missing one or more paranthesis in Actor Line", segment);
						} else {
							pose = tokens[1].Substring(tokens[1].IndexOf('(') + 1, tokens[1].IndexOf(')') - tokens[1].IndexOf('(') - 1);
						}
					} else {
						//Pose is default
						pose = "Normal";
					}

					if (subSegments.Length > 1) {
						//A line exists (otherwise line is just "")
						for (int i = 1; i < subSegments.Length; i++) {
							line += subSegments[i] + '\n';
						}
					}

					return new ActorLine(actorName, pose, line);
				} else {
					throw new InvalidScreenplaySyntaxException("Invalid number of tokens for Actor Line", segment);
				}
			}
		}

		//split the text into smaller chunks that each represent a single cutscene object
		//string segments are separated by two new line chars.
		private string[] splitText(string cutsceneText) {
			List<string> segments = new List<string>();
			int start = 0;
			for (int i = 0; i < cutsceneText.Length - 2; i++) {
				if (cutsceneText.Substring(i, 3).Equals("\n\r\n")) {
					segments.Add(cutsceneText.Substring(start, i - start));
					start = i + 3;
				}
			}

			List<string> purifiedSegments = new List<string>();
			foreach(string segment in segments) {
				purifiedSegments.Add(segment.Replace("\r", ""));
			}

			return purifiedSegments.ToArray();
		}
	}
}