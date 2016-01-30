using UnityEngine;
using System.Collections;

public class TestMovementChar : MonoBehaviour {

	Vector2 footForce = new Vector2(15f,0);
	Vector2 driftForce = new Vector2(10f,0);
	Vector2 jumpForce = new Vector2(0f,300f);
	float horizontalMaxVelocity = 5f;
	float jumpMaxVelocity = 200f;
	float fallMaxVelocity = -400f;
	float slopeAscentMaxVelocity = 2f;
	
	public bool jumping = true;

	//public RectTransform rt;
	Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
	 	//rt = (RectTransform)transform;
	 	rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//handle jump key
		if(Input.GetKey(KeyCode.UpArrow) && ! jumping){
			rigid.AddForce(jumpForce);
			jumping = true;
		}
		//handle horiz. arrow keys
		Vector2 moveForce = footForce;
		if(jumping)
			moveForce = driftForce;		
		if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
			if(rigid.velocity.x > 0)
				rigid.velocity = new Vector2(0f,rigid.velocity.y);
			rigid.AddForce(-moveForce);
		}else if(!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)){
			if(rigid.velocity.x < 0)
				rigid.velocity = new Vector2(0f,rigid.velocity.y);
			rigid.AddForce(moveForce);
		}else{ //friction
			if(Mathf.Abs(rigid.velocity.x) < horizontalMaxVelocity/5f){
				rigid.velocity = new Vector2(0f,rigid.velocity.y);
			}else if(rigid.velocity.x < 0){
				rigid.AddForce(footForce);
			}else if(rigid.velocity.x > 0){
				rigid.AddForce(-footForce);
			}
		}
		//clip velocity
		if(rigid.velocity.x > horizontalMaxVelocity){//clip x
			rigid.velocity = new Vector2(horizontalMaxVelocity,rigid.velocity.y);
		}else if(rigid.velocity.x < -horizontalMaxVelocity){
			rigid.velocity = new Vector2(-horizontalMaxVelocity,rigid.velocity.y);
		}
		if(rigid.velocity.y > jumpMaxVelocity){//clip y
			rigid.velocity = new Vector2(rigid.velocity.x,jumpMaxVelocity);
		}else if(rigid.velocity.y < fallMaxVelocity){
			rigid.velocity = new Vector2(rigid.velocity.x,fallMaxVelocity);
		}
		//stop ramps from launching us
		if(!jumping && rigid.velocity.y > slopeAscentMaxVelocity){
			rigid.velocity = new Vector2(rigid.velocity.x,slopeAscentMaxVelocity);
		}
		
		
	}
	void Update(){
		//clip position to ints
		//rt.anchoredPosition = new Vector2(Mathf.Round(rt.anchoredPosition.x),Mathf.Round(rt.anchoredPosition.y));
	}
	
	void OnCollisionEnter2D(Collision2D coll){
		jumping = false;
	}
}
