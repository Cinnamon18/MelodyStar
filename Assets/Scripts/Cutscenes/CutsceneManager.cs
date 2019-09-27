using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene
{
	public class CutsceneManager : MonoBehaviour
	{
		public TextAsset writing;

		// Start is called before the first frame update
		void Start()
		{
			List<CutsceneObject> objects = CutsceneParser.parse(writing.text);

			foreach(CutsceneObject obj in objects)
			{
				obj.Log();
			}
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}

