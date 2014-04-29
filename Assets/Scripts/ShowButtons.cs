using UnityEngine;
using System.Collections;

public class ShowButtons : MonoBehaviour 
{

	Touch test;
	GameObject player = null;
	Rect scores;
	Rect restart;
	Rect nameSpace;
	bool checkedScore = false;
	string scoreList;
	string username = "Enter Name for High-Scores";
	bool pressed = true;
	bool waiting = false;
	public GUIStyle buttonStyle, textBoxStyle; 

	// Use this for initialization
	void Start ()
	{
		nameSpace = new Rect(10, 20, Screen.width - 20, 100);
		restart = new Rect(10, Screen.height - 160,(Screen.width / 2) - 15, 120);
		scores = new Rect((Screen.width / 2) + 5, Screen.height - 160, (Screen.width / 2) - 15, 120);
		player = GameObject.Find("Gustavo");
		//this.gameObject.GetComponent<Scores>().clearData();
		//Debug.Log("x limits: 10 - " + ((Screen.width / 2) - 10) + " and " + ((Screen.width/2) + 10) + " - " + (Screen.width - 5));
	}

	void Update(){
		if(Input.touchCount > 0)
		{
			if(!pressed && !waiting)
			{
				pressed = true;
				test = Input.GetTouch(0);

				//Debug.Log("pressed pixel: " + test.position.x + ", " + test.position.y);
				if(test.position.y >= 40 && test.position.y <= 160)
				{
					if(!checkedScore && test.position.x >= 10 && test.position.x <= (Screen.width/2)-5)
					{
						checkedScore = false;
						player.GetComponent<StopScripts>().setGameStatus(2);
						if(string.Compare(username, "Enter Name for High-Scores") != 0)
						{

							this.gameObject.GetComponent<Scores>().addUser(username, player.GetComponent<StopScripts>().getScore());
						}
						this.gameObject.GetComponent<ShowButtons>().enabled = false;

					}
					else if(checkedScore && test.position.x >= 10 && test.position.x <= Screen.width - 10)
					{
						checkedScore = false;
						player.GetComponent<StopScripts>().setGameStatus(2);
						restart = new Rect(10, Screen.height - 160,(Screen.width / 2) - 15, 120);
						player.GetComponent<StopScripts>().hideScores();
						this.gameObject.GetComponent<ShowButtons>().enabled = false;
					}
					else if(test.position.x >= (Screen.width / 2) + 10 && test.position.x <= Screen.width - 5)
					{
						StartCoroutine(WaitToSwitch());
					}
				}
				else
				{
					StartCoroutine(pressWait());
				}
			}
		}
		else
		{
			pressed = false;

		}
	}

	void OnGUI()
	{
		GUI.Button(restart, "Recompile!", buttonStyle);
		if(!checkedScore)
		{
			GUI.Button(scores, "High-Scores", buttonStyle);
			username = GUI.TextField(nameSpace , username, 30, textBoxStyle);
		}
	}

	IEnumerator WaitToSwitch()
	{
		if(string.Compare(username, "Submit Score") != 0)
		{
			this.gameObject.GetComponent<Scores>().addUser(username, player.GetComponent<StopScripts>().getScore());
		}
		yield return new WaitForSeconds(.2f);

		this.gameObject.GetComponent<Scores>().showData();

		yield return new WaitForSeconds(.2f);

		scoreList = this.gameObject.GetComponent<Scores>().getScores();
		restart = new Rect(10, Screen.height - 160, Screen.width - 20, 120);
		checkedScore = true;
		player.GetComponent<StopScripts>().displayScores(scoreList);
	}

	IEnumerator pressWait(){
		waiting = true;
		yield return new WaitForSeconds(.1f);
		waiting = false;
	}
}
