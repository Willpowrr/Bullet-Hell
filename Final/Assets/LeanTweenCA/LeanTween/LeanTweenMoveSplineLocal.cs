using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("LeanTween")]
	[Tooltip("Move a GameObject through a set of points, in local space")]
	public class LeanTweenMoveSplineLocal : FsmStateAction
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
		
		[RequiredField]
		[Tooltip("A set of points that define the curve(s) ex: ControlStart,Pt1,Pt2,Pt3,.. ..ControlEnd")]
		public FsmVector3[] pathPoints;
		
		[Tooltip("time The time to complete the tween in")]
		public FsmFloat time;
		
		[ActionSection("Optional Parameters")]
		
		[Tooltip("Set orient to path")]
		public FsmBool orientToPath;
		
		[Tooltip("Set orient Axis")]
		public FsmVector3 axis;
		
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
		
		[ActionSection("Get Reference")]
		public FsmInt LeanTweenID;
		
		public override void Reset()
		{
			gameObject = null;
			time = 0f;
			delay = 0f;
			noOfRepeat = 0;
			useEstimatedTime = false;
			useFrames = false;
			orientToPath = false;
			axis = Vector3.zero;
			
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
			
			var len = pathPoints.Length;
			
			Vector3[] tweenVector = new Vector3[len];
			
			for (var i=0; i<len; i++) {
				tweenVector [i] = pathPoints [i].Value;
			}
			
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

			Fsm.Event (onStartEvent);
			
			LTDescr tween  = LeanTween.moveSplineLocal(go, tweenVector, time.Value);
			
			LeanTweenID.Value = tween.id;
			
			tween.setOrientToPath(orientToPath.Value);
			tween.setAxis(axis.Value);
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