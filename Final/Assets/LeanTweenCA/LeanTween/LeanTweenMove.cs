using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("LeanTween")]
	[Tooltip("Move a GameObject to a certain location")]
	public class LeanTweenMove : FsmStateAction
	{
		public enum LTLoop
			{
				once,
				clamp,
				pingpong
			}
		[RequiredField]
		[Tooltip("Gameobject that you wish to move.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Move To a gameobject")]
		public FsmGameObject targetGameObject;

		[RequiredField]
		[Tooltip("To The final position with which to move to")]
		public FsmVector3 vector;

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

		public FsmGameObject setGameObjectData;
		public FsmInt setIntData;
		public FsmFloat setFloatData;
		public FsmString setStringData;
		public FsmBool setBoolData;
		public FsmVector2 setVector2Data;
		public FsmVector3 setVector3Data;
		public FsmRect setRectData;
		public FsmQuaternion setQuaternionData;
		public FsmColor setColorData;
		public FsmMaterial setMaterialData;
		public FsmTexture setTextureData;
		public FsmObject setObjectData;

		private Vector3 tempVector;

		[ActionSection("Get Reference")]
		public FsmInt LeanTweenID;

		public override void Reset()
		{
			gameObject = null;

			vector = Vector3.zero;
			time = 0f;
			delay = 0f;
			noOfRepeat = 0;
			useEstimatedTime = false;
			useFrames = false;
			targetGameObject = null;
			setGameObjectData = new FsmGameObject{UseVariable = true};
			setIntData = new FsmInt { UseVariable = true };
			setFloatData = new FsmFloat { UseVariable = true };
			setStringData = new FsmString { UseVariable = true };
			setBoolData = new FsmBool { UseVariable = true };
			setVector2Data = new FsmVector2 { UseVariable = true };
			setVector3Data = new FsmVector3 { UseVariable = true };
			setRectData = new FsmRect { UseVariable = true };
			setQuaternionData = new FsmQuaternion { UseVariable = true };
			setColorData = new FsmColor { UseVariable = true };
			setMaterialData = new FsmMaterial { UseVariable = true };
			setTextureData = new FsmTexture { UseVariable = true };
			setObjectData = new FsmObject { UseVariable = true };

			onUpdateEventTarget = null;
			onUpdateEvent = null;
		}
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

			GameObject tGo = targetGameObject.Value;

			if (tGo != null) 
			{
				tempVector= tGo.transform.position;
			}
			else
			{
				tempVector = vector.Value;
			}
				Fsm.Event (onStartEvent);
		
				LTDescr tween  = LeanTween.move(go, tempVector, time.Value);

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
			Fsm.EventData.BoolData = setBoolData.Value;
			Fsm.EventData.IntData = setIntData.Value;
			Fsm.EventData.FloatData = setFloatData.Value;
			Fsm.EventData.Vector2Data = setVector2Data.Value;
			Fsm.EventData.Vector3Data = setVector3Data.Value;
			Fsm.EventData.StringData = setStringData.Value;
			Fsm.EventData.GameObjectData = setGameObjectData.Value;
			Fsm.EventData.RectData = setRectData.Value;
			Fsm.EventData.QuaternionData = setQuaternionData.Value;
			Fsm.EventData.ColorData = setColorData.Value;
			Fsm.EventData.MaterialData = setMaterialData.Value;
			Fsm.EventData.TextureData = setTextureData.Value;
			Fsm.EventData.ObjectData = setObjectData.Value;

			Fsm.Event(onUpdateEventTarget, onUpdateEvent);
			return;
		}

	}

}