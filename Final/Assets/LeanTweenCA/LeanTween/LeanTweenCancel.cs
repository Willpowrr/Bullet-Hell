using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Cancel tweens with specific ID for a GameObject")]
	public class LeanTweenCancel : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Gameobject that you wish to cancel tween.")]
		public FsmOwnerDefault gameObject;

		public FsmInt LeanTweenID;

		// Code that runs on entering the state.
		public override void OnEnter()
		{

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

			LeanTween.cancel (go, LeanTweenID.Value);

			Finish();
		}
		
		
	}
	
}
