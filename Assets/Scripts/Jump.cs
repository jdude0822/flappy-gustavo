using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	Vector3 jump = new Vector3(0,11f,0);
	Vector3 fall = new Vector3(0,-50f,0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0){
			this.rigidbody.velocity = jump;
		}
		else{
			this.rigidbody.AddForce(fall);
		}
	}
}
