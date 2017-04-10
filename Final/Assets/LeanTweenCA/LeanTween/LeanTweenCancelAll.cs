using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Cancel all tweens for a GameObject")]
	public class LeanTweenCancelAll : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{

			LeanTween.cancelAll (true);
			
			Finish();
		}
		
		
	}
	
}
