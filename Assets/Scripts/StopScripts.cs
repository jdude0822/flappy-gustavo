using UnityEngine;
using System.Collections;


public class StopScripts : MonoBehaviour {

	Vector3 fall = new Vector3(0,-30f,0);
	Vector3 stop = new Vector3 (0, 0, 0);
	int gameStarted = 0;
	Object[] blocks;
	GUIText[] labels = new GUIText[12];
	int score = 0;
	bool isDead;

	public GameObject text;
	public GameObject text_back;
	public GameObject mainCamera;

	public GameObject score_text;
	public GameObject score_text_back;
	public GameObject title_text;
	public GameObject title_text_back;
	public GameObject final_score_text;
	public GameObject final_score_text_back;
	public GameObject high_text;
	public GameObject high_text_back;

	// Use this for initialization
	void Start () {
		GameObject n = (GameObject) Instantiate(text);
		//GameObject o = (GameObject) Instantiate(text);
		GameObject p = (GameObject) Instantiate(high_text);
		GameObject score_obj = (GameObject) Instantiate(score_text);
		GameObject title_obj = (GameObject) Instantiate(title_text);
		GameObject final_score_obj = (GameObject) Instantiate(final_score_text);
		//GameObject q = (GameObject) Instantiate(text);

		GameObject n_back = (GameObject) Instantiate(text_back);
		//GameObject o_back = (GameObject) Instantiate(text_back);
		GameObject p_back = (GameObject) Instantiate(high_text_back);
		GameObject score_obj_back = (GameObject) Instantiate(score_text_back);
		GameObject title_obj_back = (GameObject) Instantiate(title_text_back);
		GameObject final_score_back_obj = (GameObject) Instantiate(final_score_text_back);


		labels[0] = n.guiText;
		//labels[1] = o.guiText;
		labels[2] = p.guiText;
		//labels[3] = q.guiText;

		labels[3] = n_back.guiText;
		//labels[4] = o_back.guiText;
		labels[5] = p_back.guiText;

		labels[6] = score_obj.guiText;
		labels[7] = score_obj_back.guiText;

		labels[8] = title_obj.guiText;
		labels[9] = title_obj_back.guiText;

		labels[10] = final_score_obj.guiText;
		labels[11] = final_score_back_obj.guiText;

		labels[9].transform.position = new Vector3(.5f, .7f, 0);
		labels[9].text = "Flappy\nGustavo";
		labels[8].transform.position = new Vector3(.5f, .7f, 0);
		labels[8].text = "Flappy\nGustavo";

		labels[3].transform.position = new Vector3(.5f, .16f, 0);
		labels[3].text = "Touch to\nCompile!";
		//labels[4].transform.position = new Vector3(.5f, .37f, 0);
		labels[5].transform.position = new Vector3(.5f, .5f, 0);

		labels[0].transform.position = new Vector3(.5f, .16f, 0);
		labels[0].text = "Touch to\nCompile!";
		//labels[1].transform.position = new Vector3(.5f, .37f, 0);
		labels[2].transform.position = new Vector3(.5f, .5f, 0);

		labels[11].transform.position = new Vector3(.5f, .39f, 0);
		labels[10].transform.position = new Vector3(.5f, .39f, 0);


		isDead = false;
	}

	// Update is called once per frame
	void Update () {
		if(gameStarted == 0 && Input.touchCount > 0){
			gameStarted = 1;
			isDead = false;
			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			this.transform.rotation = Quaternion.identity;
			this.gameObject.GetComponent<Jump>().enabled = true;
			this.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
			mainCamera.GetComponent<CreateBlocks>().enabled = true;
			//labels[3].transform.position = new Vector3(.5f, .15f, 0);
			//labels[0].transform.position = new Vector3(.5f, .15f, 0);
			labels[7].transform.position = new Vector3(.5f, .8f, 0);
			labels[6].transform.position = new Vector3(.5f, .8f, 0);
			score = 0;
			labels[3].text = "";
			labels[0].text = "";
			labels[9].text = "";
			labels[8].text = "";

			hideScores();

			labels[7].text = "" + score;
			labels[6].text = "" + score;


			this.gameObject.GetComponent<Jump>().resetJump();

		}
		else if(gameStarted == 2 && Input.touchCount > 0){
			this.gameObject.GetComponent<Jump>().resetJump();
			isDead = false;
			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			this.transform.position = new Vector3(.4f, 3.5f, 0);
			this.transform.rotation = Quaternion.identity;
			foreach(GameObject g in blocks){
				if(g != null){
					GameObject.Destroy(g);
				}
			}

			hideScores();

			labels[9].text = "Flappy\nGustavo";
			labels[8].text = "Flappy\nGustavo";

			labels[0].transform.position = new Vector3(.5f, .16f, 0);
			labels[3].transform.position = new Vector3(.5f, .16f, 0);

			labels[3].text = "Touch to\nCompile!";
			//labels[4].text = "";

			labels[0].text = "Touch to\nCompile!";
			//labels[1].text = "";

			labels[7].text = "";
			labels[6].text = "";
			//labels[2].text = "";

			labels[11].text = "";
			labels[10].text = "";
			StartCoroutine(restartWait2());
		}

		if(isDead){
			this.rigidbody.AddForce(fall);
		}
	}

	void OnCollisionEnter(Collision col){
		if(!isDead){
			isDead = true;
			this.rigidbody.velocity = stop;
			this.transform.Rotate (Vector3.up * 180);
		
			this.gameObject.GetComponent<Jump>().enabled = false;
			blocks = GameObject.FindGameObjectsWithTag("Blocks");
			foreach(GameObject g in blocks){
				if(g.GetComponent<MoveBlocks>() != null){
					g.GetComponent<MoveBlocks>().enabled = false;
				}
			}
			//this.gameObject.GetComponent<Jump>().enabled = false;
			//this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			//Debug.Log(mainCamera);
			mainCamera.GetComponent<CreateBlocks>().enabled = false;

			labels[3].transform.position = new Vector3(.5f, .54f, 0);
			labels[3].text = "Compilation\n   Over!";
			//labels[4].text = "Score = " + score;

			labels[0].transform.position = new Vector3(.5f, .54f, 0);
			labels[0].text = "Compilation\n   Over!";
			//labels[1].text = "Score = " + score;

			labels[11].text = "Score=" + score;
			labels[10].text = "Score=" + score;

			labels[7].text = "";
			labels[6].text = "";
			//mainCamera.GetComponent<Scores>().addUser("Gustavo", score);

			//StartCoroutine(restartWait());
			mainCamera.GetComponent<ShowButtons>().enabled = true;
		}
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
		labels[7].text = "" + score;
		labels[6].text = "" + score;
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
		labels[7].text = "";
		labels[6].text = "";

		//labels[3].text = "";
		//labels[4].text = "";
		//labels[0].text = "";
		//labels[1].text = "";

		labels[11].text = "";
		labels[10].text = "";

		labels[3].transform.position = new Vector3(.5f, .85f, 0);
		labels[3].text = "High-Scores";
		labels[0].transform.position = new Vector3(.5f, .85f, 0);
		labels[0].text = "High-Scores";
		
		labels[5].text = scores;
		labels[2].text = scores;	
	}

	public void hideScores(){
		labels[5].text = "";
		labels[2].text = "";
	}
}
