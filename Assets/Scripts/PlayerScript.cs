using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	GameObject warp1, warp2;
	int number = 1;
	bool is_Warp = false;
	Camera camera;
	Vector3 startPos;
	float speed = 0;
	Rigidbody rb;


	// Use this for initialization
	void Start () {
		warp1 = GameObject.Find ("Warp");
		warp2 = GameObject.Find ("Warp2");
		is_Warp = false;
		camera = transform.FindChild ("MainCamera").GetComponent<Camera>();
		number = 1;
		startPos = transform.position;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.state == GameManager.State.Game) {
			if (Input.GetMouseButtonDown (0)) {
				Shot ();
			}
			if (transform.position.y < -10) {
				transform.position = startPos;
			}
		}else if(GameManager.instance.state == GameManager.State.Goal){

		}
		
	
	}

	void OnTriggerEnter(Collider col){
		if (GameManager.instance.state == GameManager.State.Game) {
			if (col.tag == "Warp") {
				if (!is_Warp) {
					col.transform.parent.parent.GetComponent<Collider> ().isTrigger = true;
				}
			} else if (col.tag == "Goal") {
				GameManager.instance.ToGoal ();
			}
		} else if (GameManager.instance.state == GameManager.State.Goal) {

		}

	}
	void OnTriggerExit(Collider col){
		if (GameManager.instance.state == GameManager.State.Game) {
			if (col.tag == "Warp") {
				if (!is_Warp) {

					col.transform.parent.parent.GetComponent<Collider> ().isTrigger = false;
					int id = col.transform.parent.GetComponent<WarpScript> ().id;
					speed = rb.velocity.magnitude;

					if (id == 1) {
				
						transform.position = warp2.transform.position;
						rb.velocity = warp2.transform.up.normalized * speed * 0.3f;
						transform.LookAt (warp2.transform.position + warp2.transform.up * 10);
						//transform.forward = warp2.transform.up;



					} else {
						transform.position = warp1.transform.position;
						rb.velocity = warp1.transform.up.normalized * speed * 0.3f;
						//transform.forward = warp1.transform.up;
						transform.LookAt (warp1.transform.position + warp1.transform.up * 10);
					}
					is_Warp = true;
				} else {
					is_Warp = false;
				}

			}
		} else if (GameManager.instance.state == GameManager.State.Goal) {

		}

	}

	void Shot(){
		Ray ray = camera.ScreenPointToRay (new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			if (hit.collider.tag == "Stage") {
				if (number == 1) {

					if (Vector3.Distance (hit.point, warp2.transform.position) > 8) {
						warp1.transform.parent = null;
						warp1.transform.position = hit.point + hit.normal.normalized * (warp1.transform.localScale.y / 2);
						warp1.transform.up = hit.normal.normalized;
						warp1.transform.parent = hit.transform;
						NextNumber ();
					}
				} else {
					if (Vector3.Distance (hit.point, warp1.transform.position) > 8) {
						warp2.transform.parent = null;
						warp2.transform.position = hit.point + hit.normal.normalized * (warp2.transform.localScale.y / 2);
						warp2.transform.up = hit.normal.normalized;
						warp2.transform.parent = hit.transform;
						NextNumber ();
					}
				}
			}
		}
	}
	void NextNumber(){
		number++;
		if (number > 2) {
			number = 1;
		}
	}

	public void ToStartPos(){
		transform.position = startPos;
		warp1.transform.position = Vector3.one * -1000;
		warp2.transform.position = Vector3.one * -1100;

	}
}
