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
		 */
		public static List<CutsceneObject> parse(string cutsceneText)
		{
			//The list of cutscene objects
			List<CutsceneObject> objects = new List<CutsceneObject>();

			//Each one of these represents an individual object (after parsing)
			string[] segments = splitText(cutsceneText);

			foreach(string segment in segments)
			{
				Debug.Log(segment);
				//need to get each individual line in a segment
				string[] subSegments = segment.Split('\n');

				/***
				 * Assuming for now that every segment is dialogue, but this may change.
				 * If it does change, there just needs to be some form of identifier on the first line
				 * ex:
				 * switch(identifier) {
				 *	case(d) : 
				 *		do dialogue stuff
				 *		break;
				 *	case(s) :
				 *		do scene transition
				 *		break;
				 * }
				 */

				string actor = subSegments[0];

				string pose;
				string line = "";

				//if the actor has a pose, add it, otherwise pose defaults to a neutral pose
				if (subSegments[1][0].Equals('('))
				{
					pose = subSegments[1].Substring(subSegments[1].IndexOf('(') + 1, subSegments[1].IndexOf(')') - subSegments[1].IndexOf('(') - 1);
				} else
				{
					pose = "neutral";
					line = subSegments[1];
				}

				for(int i = 2; i < subSegments.Length; i++)
				{
					line += subSegments[i];	
				}

				objects.Add(new ActorLine(actor, pose, line));
			}

			return objects;
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
					Debug.Log("Yes");
					segments.Add(cutsceneText.Substring(start, i - start));
					start = i + 3;
				}
			}


			return segments.ToArray();
		}
	}
}