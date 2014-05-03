using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}

	/**
	 * Returns the www object, use a coroutine to wait for it to return before using.
	 */
	public WWW showData(){
		string url = "http://data.cs.purdue.edu:1460/Sql/score.cgi";
		
		WWWForm form = new WWWForm();
		form.AddField("a", "get");
		WWW www = new WWW(url, form);

		return www;
		//StartCoroutine(WaitForRequestShow(www));
	}

	/**
	 * Returns the www object, use a coroutine to wait for it to return before using.
	 */
	public WWW addUser(string user, int score){
		string url = "http://data.cs.purdue.edu:1460/Sql/score.cgi";
		
		WWWForm form = new WWWForm();
		form.AddField("a", "add");
		form.AddField("u", user);
		form.AddField("s", score);
		WWW www = new WWW(url, form);

		return www;
		//StartCoroutine(WaitForRequestAdd(www));
	}

	public void clearData(){
		string url = "http://data.cs.purdue.edu:1460/Sql/score.cgi";
		
		WWWForm form = new WWWForm();
		form.AddField("a", "clear");
		//form.AddField("var2", "value2");
		WWW www = new WWW(url, form);
		
		StartCoroutine(WaitForRequestShow(www));
	}

	IEnumerator WaitForRequestAdd(WWW www)
	{
		yield return www;
			// check for errors
		if (www.error == null) {
			Debug.Log("Add Ok!: " + www.text);
		}
		else {
			Debug.Log("Add Error: "+ www.error);
		}
	}

	IEnumerator WaitForRequestShow(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null) {
			Debug.Log("Show Ok!: " + www.text);
		}
		else {
			Debug.Log("Show Error: "+ www.error);
		}
	}

	IEnumerator WaitForRequestClear(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null) {
			Debug.Log("Clear Ok!: " + www.text);
		}
		else {
			Debug.Log("Clear Error: "+ www.error);
		}
	}
}
