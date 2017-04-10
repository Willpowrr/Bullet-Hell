using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Rotate a GameObject only on the Y axis")]
	public class LeanTweenValue : FsmStateAction
	{
		public enum LTLoop
		{
			once,
			clamp,
			pingpong
		}
		[RequiredField]
		[Tooltip("GameObject with which to tie the tweening with")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("from The original value to start the tween from")]
		public FsmFloat fromValue;

		[RequiredField]
		[Tooltip("to The value to end the tween on")]
		public FsmFloat toValue;
		
		[Tooltip("time The time to complete the tween in")]
		public FsmFloat time;
		
		[ActionSection("Optional Parameters")]
		
		[Tooltip("Set the type of easing used for the tween")]
		public LeanTweenType easeType;
		
		[Tooltip("Delay the start of a tween")]
		public FsmFloat delay;
		
		[Tooltip("Set the tween to repeat a number of times")]
		public FsmInt noOfRepeat;
		
		[Tooltip("Set Loop Type of repeat")]
		public LTLoop LoopType;
		
		[Tooltip("Use estimated time when tweening an object.")]
		public FsmBool useEstimatedTime;
		
		[Tooltip("Use frames when tweening an object")]
		public FsmBool useFrames;
		
		[ActionSection("Send Event")]

		[Tooltip("Have a Event called when the tween starts")]
		public FsmEvent onStartEvent;
		
		[Tooltip("Have a Event called when the tween finishes")]
		public FsmEvent onCompleteEvent;

		[ActionSection("Output")]
		public FsmFloat tweenValue;

		[ActionSection("Get Reference")]
		public FsmInt LeanTweenID;
		
		public override void Reset()
		{
			gameObject = null;
			fromValue = 0f;
			toValue = 0f;
			time = 0f;
			delay = 0f;
			noOfRepeat = 0;
			useEstimatedTime = false;
			useFrames = false;
			
			}
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			Fsm.Event (onStartEvent);
			LTDescr tween  = LeanTween.value(go, doOnUpdate, fromValue.Value, toValue.Value, time.Value);
			
			LeanTweenID.Value = tween.id;
			
			tween.setEase(easeType);
			tween.setDelay (delay.Value);
			
			if (noOfRepeat.Value > 0) tween.setRepeat(noOfRepeat.Value);
			
			switch (LoopType)
			{
			case LTLoop.clamp:
				tween.setLoopClamp();
				break;
			case LTLoop.once:
				tween.setLoopOnce();
				break;
			case LTLoop.pingpong:
				tween.setLoopPingPong();
				break;
			}
			
			tween.setOnComplete(doOnComplete);
			tween.setOnUpdate(doOnUpdate);
			tween.setUseEstimatedTime(useEstimatedTime.Value);
			tween.setUseFrames(useFrames.Value);

		}
		
		public void doOnComplete()
		{
			Fsm.Event (onCompleteEvent);
			return;
		}
		
		public void doOnUpdate(float tween)
		{
			tweenValue.Value = tween;
			return;
		}
		
	}
	
}