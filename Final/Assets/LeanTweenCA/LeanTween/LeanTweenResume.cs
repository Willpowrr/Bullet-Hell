using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Resume a specific tween")]
	public class LeanTweenResume : FsmStateAction
	{
		public FsmInt LeanTweenID;
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			LeanTween.resume(LeanTweenID.Value);
			Finish();
		}
		
		
	}
	
}
