using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Cancel all tweens for a GameObject")]
	public class LeanTweenPauseAll : FsmStateAction
	{
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			
			LeanTween.pauseAll ();
			
			Finish();
		}
		
		
	}
	
}
