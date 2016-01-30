using UnityEngine;
using UnityEngine.UI;

public class CanvasColliderEgg : MonoBehaviour{
	public static readonly int TOP_LEFT = 0;
	public static readonly int TOP_RIGHT = 0;
	public static readonly int BOTTOM_RIGHT = 0;
	public static readonly int BOTTOM_LEFT = 0;
	
	public bool preShaped = false;
	public bool presetRectangle = false;
	public int presetSlopeMissingCorner = 0;
	
	RectTransform rt;
	
	public void Start(){
		rt = GetComponent<RectTransform>();
		if(preShaped){
			if(presetRectangle){
				HatchBoxCollider();
			}else{
				HatchSlopeCollider(presetSlopeMissingCorner);
			}
		}
	}
	
	public void HatchBoxCollider(){
		BoxCollider2D boxy = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
		boxy.size = rt.sizeDelta;
	}
	public void HatchSlopeCollider(int missingCorner){ //0=top left, clockwise through 3
		if(missingCorner < 0 || missingCorner > 3){ //bounds check
			return;
		}
		PolygonCollider2D slope = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D; //instantiate collider
		Vector2[] points = new Vector2[4];
		points[0] = new Vector2(rt.rect.min.x,rt.rect.max.y);//top left
		points[1] = rt.rect.max;//top right
		points[2] = new Vector2(rt.rect.max.x,rt.rect.min.y);//bottom right
		points[3] = rt.rect.min;//bottom left
		Vector2[] culledPoints = new Vector2[3];
		int index = 0;
		for(int i = 0; i < 4; i++){
			if(i != missingCorner){ //skip the non-included corner
				culledPoints[index] = points[i];
				index++;
			}
		}
		slope.points = culledPoints; //manually set collider points
	}
}

