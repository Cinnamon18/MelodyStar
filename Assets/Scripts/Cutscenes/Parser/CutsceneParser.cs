using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cutscene {
	public class CutsceneParser {
		//read from file, tokenize, ~do magic~

		/* Kinda useless because Unity has text assets that do the job for you
		public static string getFileText(string path) 
		{
			if (File.Exists(path))
			{
				string s;
				// Open the file to read from.
				using (StreamReader sr = File.OpenText(path))
				{
					s = sr.ReadToEnd();
				}

				return s;
			} else
			{
				Debug.LogError("File does not exist. Path specified could have been invalid.");
				return "";
			}
		}
		*/

		/*
		private static List<string> tokenize(string cutsceneText) {
			//tokens has all of our tokens
			List<string> tokens = new List<string>();


			foreach (string cutsceneLine in cutsceneLines)
			{
				string actorPose = cutsceneLine.Substring(0, cutsceneLine.IndexOf(':'));
				string actor = actorPose.Substring(0, cutsceneLine.IndexOf('('));
				string pose = actorPose.Substring(cutsceneLine.IndexOf('('), cutsceneLine.IndexOf(')') - cutsceneLine.IndexOf('('));

				string body = cutsceneLine.Substring(cutsceneLine.IndexOf(':'), cutsceneLine.Length - cutsceneLine.IndexOf(':'));
				tokens.Add(new ActorLine(actor, pose, body));
			}

			return tokens;
		}
		*/

		/*
		 * Takes in the cutscene text and returns a list of cutscene objects
		 * Note: If there are any syntax errors in the text, null will be returned and bad things will happen :(
		 */
		public static List<CutsceneObject> parse(string cutsceneText)
		{
			//The list of cutscene objects
			List<CutsceneObject> objects = new List<CutsceneObject>();

			//Each one of these represents an individual object (after parsing)
			string[] segments = splitText(cutsceneText);

			foreach (string segment in segments)
			{
				CutsceneObject newCutsceneObject;
				try
				{
					newCutsceneObject = BuildObject(segment);
					objects.Add(newCutsceneObject);
				} catch(InvalidScreenplaySyntaxException ex)
				{
					Debug.LogError(ex.Message);
					//newCutsceneObject = new ActorLine("George P. Burdell", "Shocked", "Some noob messed up the script syntax.");
					//objects.Add(newCutsceneObject);
				}
			}

			return objects;
		}

		private static CutsceneObject BuildObject(string segment)
		{
			Debug.Log(segment);
			//need to get each individual line in a segment
			string[] subSegments = segment.Split('\n');

			string header = subSegments[0];

			string[] tokens = header.Split(' ');


			if (tokens[0].Contains("Scene:"))
			{
				//Scene transition
				if (tokens.Length == 2)
				{
					string sceneName = tokens[1];
					return new SceneTransition(sceneName);
				} else
				{
					throw new InvalidScreenplaySyntaxException("Invalid number of tokens for Scene Transition");
				}
			} else if (header.Contains("*"))
			{
				//Actor Action
				if (tokens.Length == 2)
				{
					string actorName = tokens[0].Substring(tokens[0].IndexOf('*') + 1, tokens[0].Length - 1);
					string action = tokens[1];

					return new ActorAction(actorName, action);
				} else
				{
					throw new InvalidScreenplaySyntaxException("Invalid number of tokens for Actor Action");
				}

			} else
			{
				//Actor Line
				string actorName = tokens[0];
				string pose;
				string line = "";

				if ((tokens.Length == 2) || (tokens.Length == 1))
				{
					if (tokens.Length == 2)
					{
						//Pose is given
						if ((tokens[1].IndexOf('(') == -1) || (tokens[1].IndexOf(')') == -1))
						{
							throw new InvalidScreenplaySyntaxException("missing one or more paranthesis in Actor Line");
						} else
						{
							pose = tokens[1].Substring(tokens[1].IndexOf('(') + 1, tokens[1].IndexOf(')') - tokens[1].IndexOf('(') - 1);
						}

					}
					else
					{
						//Pose is default
						pose = "Idle";
					}

					if (subSegments.Length > 1)
					{
						//A line exists (otherwise line is just "")
						for (int i = 1; i < subSegments.Length; i++)
						{
							line += subSegments[i] + '\n';
						}
					}

					return new ActorLine(actorName, pose, line);
				}
				else
				{
					throw new InvalidScreenplaySyntaxException("Invalid number of tokens for Actor Line");
				}
			}
		}

		//split the text into smaller chunks that each represent a single cutscene object
		//string segments are separated by two new line chars.
		private static string[] splitText(string cutsceneText)
		{
			List<string> segments = new List<string>();
			int start = 0;
			for (int i = 0; i < cutsceneText.Length - 2; i++)
			{
				if (cutsceneText.Substring(i, 3).Equals("\n\r\n"))
				{
					segments.Add(cutsceneText.Substring(start, i - start));
					start = i + 3;
				}
			}

			return segments.ToArray();
		}
	}
}