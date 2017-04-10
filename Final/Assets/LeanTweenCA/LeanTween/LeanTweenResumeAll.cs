using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Resumes all tweens")]
	public class LeanTweenResumeAll : FsmStateAction
	{
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			
			LeanTween.resumeAll ();
			
			Finish();
		}
		
		
	}
	
}
