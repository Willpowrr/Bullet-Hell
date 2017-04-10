using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Rotate a GameObject around a certain Axis (the best method to use when you want to rotate beyond 180 degrees).")]
	public class LeanTweenRotateAroundLocal : FsmStateAction
	{
		public enum LTLoop
		{
			once,
			clamp,
			pingpong
		}
		[RequiredField]
		[Tooltip("Gameobject that you wish to rotate.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("axis in which to rotate around ex: Vector3.up")]
		public FsmVector3 vector;
		
		[Tooltip("the degrees in which to rotate")]
		public FsmFloat degrees;
		
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
		
		public FsmEventTarget onUpdateEventTarget;
		
		[Tooltip("Have a method called on each frame that the tween is being animated")]
		public FsmEvent onUpdateEvent;
		
		[ActionSection("Get Reference")]
		public FsmInt LeanTweenID;
		
		public override void Reset()
		{
			gameObject = null;
			vector = Vector3.up;
			degrees = 0f;
			time = 0f;
			delay = 0f;
			noOfRepeat = 0;
			useEstimatedTime = false;
			useFrames = false;
			
			onUpdateEventTarget = null;
			onUpdateEvent = null;
		}
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			Fsm.Event (onStartEvent);
			LTDescr tween  = LeanTween.rotateAroundLocal(go, vector.Value, degrees.Value, time.Value);
			
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
		
		public void doOnUpdate(float nullFloat)
		{

			Fsm.Event(onUpdateEventTarget, onUpdateEvent);
			return;
		}
		
	}
	
}