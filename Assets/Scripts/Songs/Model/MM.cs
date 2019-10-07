using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Songs.Model
{
	public class MM
	{
		public static int Multiplier(int numCorrect)
		{

			if (numCorrect > 200/10) {
				return 8;
			}
			else if (numCorrect> 100/10){
				return 4;
				}
			else if (numCorrect > 50/10){
				return 2;
			}
			else {
				return 1;
			}




			
			
			


		}

	}
}