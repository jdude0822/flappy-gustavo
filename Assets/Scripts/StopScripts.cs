using UnityEngine;
using System.Collections;


public class StopScripts : MonoBehaviour {
	
	short gameStarted = 0;
	Object[] blocks;
	GUIText[] labels = new GUIText[3];
	int score = 0;

	public GameObject text;
	public GameObject mainCamera;

	// Use this for initialization
	void Start () {
		GameObject n = (GameObject) Instantiate(text);
		GameObject o = (GameObject) Instantiate(text);
		GameObject p = (GameObject) Instantiate(text);

		//labels[0] = (GUIText)n.AddComponent(typeof(GUIText));
		//labels[1] = (GUIText)o.AddComponent(typeof(GUIText));
		//labels[2] = (GUIText)p.AddComponent(typeof(GUIText));

		labels[0] = n.guiText;
		labels[1] = o.guiText;
		labels[2] = p.guiText;

		labels[0].transform.position = new Vector3(.5f, .5f, 0);
		labels[0].text = "Harness the COMPILER!!";
		//labels[0].fontStyle = FontStyle.Bold;
		//labels[0].anchor = TextAnchor.MiddleCenter;
		//labels[0].color = Color.black;
		//labels[0].fontSize = 24;
		//labels[1].fontStyle = FontStyle.Bold;
		//labels[1].anchor = TextAnchor.MiddleCenter;
		//labels[1].color = Color.black;
		labels[1].transform.position = new Vector3(.5f, .42f, 0);
		//labels[1].fontSize = 15;
		labels[2].transform.position = new Vector3(.5f, .34f, 0);
		//labels[2].anchor = TextAnchor.MiddleCenter;
		//labels[2].fontStyle = FontStyle.Bold;
		//labels[2].color = Color.black;
		//labels[2].fontSize = 15;
		//mainCamera = GameObject.Find("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStarted == 0 && Input.touchCount > 0){
			gameStarted = 1;
			this.gameObject.GetComponent<Jump>().enabled = true;
			this.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
			mainCamera.GetComponent<CreateBlocks>().enabled = true;
			labels[0].transform.position = new Vector3(.5f, .15f, 0);
			score = 0;
			labels[0].text = "Score: " + score;
		}
		else if(gameStarted == 2 && Input.touchCount > 0){
			this.transform.position = new Vector3(.4f, 6f, 0);
			foreach(GameObject g in blocks){
				if(g != null){
					GameObject.Destroy(g);
				}
			}

			labels[0].text = "Harness the COMPILER!";
			labels[1].text = "";
			labels[2].text = "";
			StartCoroutine(restartWait2());
		}
	}

	void OnCollisionEnter(Collision col){
		blocks = GameObject.FindGameObjectsWithTag("Blocks");
		foreach(GameObject g in blocks){
			if(g.GetComponent<MoveBlocks>() != null){
				g.GetComponent<MoveBlocks>().enabled = false;
			}
		}
		this.gameObject.GetComponent<Jump>().enabled = false;
		this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		//Debug.Log(mainCamera);
		mainCamera.GetComponent<CreateBlocks>().enabled = false;
		labels[0].transform.position = new Vector3(.5f, .5f, 0);
		labels[0].text = "Compilation over!";
		labels[1].text = "Score = " + score;
		StartCoroutine(restartWait());

	}

	IEnumerator restartWait(){
		yield return new WaitForSeconds(1.5f);
		gameStarted = 2;
		labels[2].text = "Tap to recompile!";
	}

	IEnumerator restartWait2(){
		yield return new WaitForSeconds(.3f);
		gameStarted = 0;
	}

	public void incrementScore(){
		score++;
		labels[0].text = "Score: " + score;
	}
}
