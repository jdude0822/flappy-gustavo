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
	string username = "Enter Name";
	bool pressed = true;
	bool waiting = false;
	public GUIStyle buttonStyle, textBoxStyle; 

	// Use this for initialization
	void Start ()
	{
		nameSpace = new Rect(10, Screen.height * (1f/16), Screen.width - 20, Screen.height * (1f/9));
		restart = new Rect(10, Screen.height * (7f/8),(Screen.width / 2) - 15, Screen.height * (1f/9));
		scores = new Rect((Screen.width / 2) + 5, Screen.height * (7f/8), (Screen.width / 2) - 15, Screen.height * (1f/9));
		player = GameObject.Find("Gustavo");
		buttonStyle.fontSize = (int) (.06944*Screen.width);
		textBoxStyle.fontSize = (int) (.06944*Screen.width);
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
				if(test.position.y >= Screen.height * (.01389) && test.position.y <= Screen.height * (7f/8))
				{
					if(!checkedScore && test.position.x >= 10 && test.position.x <= (Screen.width/2)-5)
					{
						checkedScore = false;
						player.GetComponent<StopScripts>().setGameStatus(2);
						if(string.Compare(username, "Enter Name") != 0)
						{

							this.gameObject.GetComponent<Scores>().addUser(username, player.GetComponent<StopScripts>().getScore());
						}
						this.gameObject.GetComponent<ShowButtons>().enabled = false;

					}
					else if(checkedScore && test.position.x >= 10 && test.position.x <= Screen.width - 10)
					{
						checkedScore = false;
						player.GetComponent<StopScripts>().setGameStatus(2);
						restart = new Rect(10, Screen.height * (7f/8), (Screen.width / 2) - 15, Screen.height * (1f/9));
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
			username = GUI.TextField(nameSpace , username, 10, textBoxStyle);
		}
	}

	IEnumerator WaitToSwitch()
	{
		//yield return this.gameObject.GetComponent<Scores>().addUser(username, player.GetComponent<StopScripts>().getScore());

		//yield return this.gameObject.GetComponent<Scores>().showData();

		yield return new WaitForSeconds (.2f);
		this.gameObject.GetComponent<Scores>().showData();

		scoreList = this.gameObject.GetComponent<Scores>().getScores();
		restart = new Rect(10, Screen.height * (7f/8), Screen.width - 20, Screen.height * (1f/9));
		checkedScore = true;
		player.GetComponent<StopScripts>().displayScores(scoreList);
	}

	IEnumerator pressWait(){
		waiting = true;
		yield return new WaitForSeconds(.1f);
		waiting = false;
	}
}
