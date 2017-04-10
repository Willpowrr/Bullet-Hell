using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("LeanTween")]
	[Tooltip("Pause all tweens for a GameObject")]
	public class LeanTweenPause : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Gameobject that you wish to cancel tween.")]
		public FsmOwnerDefault gameObject;

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			LeanTween.pause (go);
			Finish();
		}


	}

}
