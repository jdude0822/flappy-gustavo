using UnityEngine;
using System.Collections;

public class CreateBlocks : MonoBehaviour {

	//float step;
	float counter = 0f;
	int height = 0;
	Vector3 start = new Vector3(8, 5, 0);

	public GameObject clone;
	public float seconds = 5f;
	
	// Use this for initialization
	void Start () {
		seconds *= 50f;
		counter = seconds;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if(counter > seconds){
			height = Random.Range(3, 8);
			start.Set(8,(float) height, 0);
			GameObject newblock = (GameObject) GameObject.Instantiate(clone, start, Quaternion.identity);
			newblock.GetComponent<MoveBlocks>().enabled = true;
			counter = 0f;
		}
		else{
			counter++;
		}
	}
}
