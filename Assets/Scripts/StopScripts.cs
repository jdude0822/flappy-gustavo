using UnityEngine;
using System.Collections;


public class StopScripts : MonoBehaviour {
	
	int gameStarted = 0;
	Object[] blocks;
	GUIText[] labels = new GUIText[4];
	int score = 0;

	public GameObject text;
	public GameObject mainCamera;


	// Use this for initialization
	void Start () {
		GameObject n = (GameObject) Instantiate(text);
		GameObject o = (GameObject) Instantiate(text);
		GameObject p = (GameObject) Instantiate(text);
		//GameObject q = (GameObject) Instantiate(text);


		labels[0] = n.guiText;
		labels[1] = o.guiText;
		labels[2] = p.guiText;
		//labels[3] = q.guiText;

		labels[0].transform.position = new Vector3(.5f, .5f, 0);
		labels[0].text = "Harness the COMPILER!!";
		labels[1].transform.position = new Vector3(.5f, .42f, 0);
		labels[2].transform.position = new Vector3(.5f, .7f, 0);

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
			//labels[2].text = "";
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
		//mainCamera.GetComponent<Scores>().addUser("Gustavo", score);

		StartCoroutine(restartWait());

	}

	IEnumerator restartWait(){
		yield return new WaitForSeconds(1.5f);

		mainCamera.GetComponent<ShowButtons>().enabled = true;

		//if(Input.GetTouch(0).position.x

		//gameStarted = 2;
		//labels[2].text = "Tap to recompile!";
	}

	IEnumerator restartWait2(){
		yield return new WaitForSeconds(.3f);
		gameStarted = 0;
	}

	public void incrementScore(){
		score++;
		labels[0].text = "Score: " + score;
	}
	
	/**
	 * status: 0-game stopped, 1- game running, 2 - game ended
	 */
	public void setGameStatus(int status){
		gameStarted = status;
	}

	public int getScore(){
		return score;
	}

	public void displayScores(string scores){
		labels[0].text = "";
		labels[1].text = "";
		labels[2].text = scores;
	}

	public void hideScores(){
		labels[2].text = "";
	}
}
