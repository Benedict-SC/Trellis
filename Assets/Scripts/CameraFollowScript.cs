using UnityEngine;

public class CameraFollowScript : MonoBehaviour{
	public Transform target = null;
	
	
	
	void Update(){
		if(target != null)
			transform.position = new Vector3(target.position.x,target.position.y+1f,transform.position.z);
	}
}
