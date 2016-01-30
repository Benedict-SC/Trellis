using UnityEngine;
using UnityEngine.UI;

public class ColliderEgg : MonoBehaviour{
	public static readonly int TOP_LEFT = 0;
	public static readonly int TOP_RIGHT = 0;
	public static readonly int BOTTOM_RIGHT = 0;
	public static readonly int BOTTOM_LEFT = 0;
	
	public bool preShaped = false;
	public bool presetRectangle = false;
	public int presetSlopeMissingCorner = 0;
	
	SpriteRenderer sr;
	
	public void Start(){
		sr = GetComponent<SpriteRenderer>();
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
		boxy.size = new Vector2(sr.bounds.size.x,sr.bounds.size.y);
	}
	public void HatchSlopeCollider(int missingCorner){ //0=top left, clockwise through 3
		if(missingCorner < 0 || missingCorner > 3){ //bounds check
			return;
		}
		PolygonCollider2D slope = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D; //instantiate collider
		Vector2[] points = new Vector2[4];
		points[0] = new Vector2(-sr.bounds.extents.x,sr.bounds.extents.y);//top left
		points[1] = new Vector2(sr.bounds.extents.x,sr.bounds.extents.y);//top right
		points[2] = new Vector2(sr.bounds.extents.x,-sr.bounds.extents.y);//bottom right
		points[3] = new Vector2(-sr.bounds.extents.x,-sr.bounds.extents.y);//bottom left
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
