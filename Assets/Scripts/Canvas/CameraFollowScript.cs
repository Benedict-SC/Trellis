using UnityEngine;
using UnityEngine.UI;

public class CameraFollowScript : MonoBehaviour{
	public Ivy target = null;
	RectTransform trt;
	RectTransform rt;
	void Start(){
		rt = GetComponent<RectTransform>();
		if(target != null){
			trt = target.GetComponent<RectTransform>();
		}
		
	}
	
	
	void Update(){
		float offset = 0f;
		if(target != null)
			rt.anchoredPosition = new Vector2(trt.anchoredPosition.x,trt.anchoredPosition.y+offset);
	}
}
