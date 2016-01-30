using UnityEngine;
using UnityEngine.UI;

public class CameraFollowScript : MonoBehaviour{
	public Ivy target = null;
	RectTransform trt;
	RectTransform rt;
	
	float offsetMult = 15f;
	float offsetStepSize = 1.5f;
	public Vector2 offset = Vector2.zero;
	
	void Start(){
		rt = GetComponent<RectTransform>();
		if(target != null){
			trt = target.GetComponent<RectTransform>();
		}
		
	}
	void Update(){
		Vector2 offsetTarget = target.GetVelocity() * offsetMult;
		Vector2 offsetDist = offsetTarget - offset;
		if(offsetDist.magnitude > offsetStepSize){
			offsetDist.Normalize();
			offsetDist *= offsetStepSize;
		}
		offset += offsetDist;
		if(target != null)
			rt.anchoredPosition = trt.anchoredPosition + offset;
	}
	/*void Update(){
		float offsetTarget = target.GetVelocity().y * offsetMult;
		if(offsetTarget > offset){
			float offdist = offsetTarget-offset;
			if(offdist > offsetStepSize)
				offdist = offsetStepSize;
			offset += offdist;
		}else if(offsetTarget < offset){
			float offdist = offsetTarget-offset;
			if(offdist < -offsetStepSize)
				offdist = -offsetStepSize;
			offset += offdist;
		}
		if(target != null)
			rt.anchoredPosition = new Vector2(trt.anchoredPosition.x,trt.anchoredPosition.y+offset);
	}*/
}
