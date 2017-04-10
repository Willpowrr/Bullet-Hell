using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Test whether or not a tween is active or not")]
	public class LeanTweenIsTweening : FsmStateAction
	{
		public FsmInt LeanTweenID;

		public FsmBool tweening;

		public bool everyFrame;


		// Code that runs on entering the state.
		public override void OnEnter()
		{
			doIsTweening ();
			if (!everyFrame) {
								Finish();
						}

		}

		public override void OnUpdate()
		{
			doIsTweening ();
			
		}

		public void doIsTweening()
		{
						tweening.Value = LeanTween.isTweening (LeanTweenID.Value);
						return;
		}
		
		
	}
	
}
