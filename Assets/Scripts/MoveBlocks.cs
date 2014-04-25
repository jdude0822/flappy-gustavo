using UnityEngine;
using System.Collections;

public class MoveBlocks : MonoBehaviour {

	bool added = false;
	GameObject player;
	//Vector3 move = new Vector3(-1f, 0, 0);

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Gustavo");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate((float)(-2.5f * Time.deltaTime), 0f, 0f);
		if(this.transform.position.x < -3){
			GameObject.Destroy(this.gameObject);
		}
		else if(!added && this.transform.position.x < -.61){
			player.GetComponent<StopScripts>().incrementScore();
			added = true;
		}
	}
}
