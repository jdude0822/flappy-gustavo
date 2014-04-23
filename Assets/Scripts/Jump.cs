using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour 
{

	Vector3 jump = new Vector3(0,13f,0);
	Vector3 fall = new Vector3(0,-40f,0);
	Vector3 velocity;
	Rigidbody player;
	bool pressed, waitActive;
	Texture2D wingsUp;
	Texture2D wingsDown;
	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Rigidbody>();
		wingsUp = Resources.Load("Gustavo wings down") as Texture2D;
		wingsDown = Resources.Load ("Gustavo wings up") as Texture2D;
		pressed = false;
		waitActive = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		velocity = player.velocity;
		if(Input.touchCount > 0)
		{
			//Debug.Log(pressed.ToString() + ": " + Input.touchCount);
			if(pressed == false && waitActive == false)
			{
				pressed = true;
				renderer.material.mainTexture = wingsUp;
				this.rigidbody.velocity = jump;				
			}

			else
			{
				if(velocity.y > -15)
				{
					this.rigidbody.AddForce(fall);
				}
				StartCoroutine(Wait());
			}
		}

		else
		{
			//Debug.Log(pressed.ToString() + ": " + Input.touchCount);
			pressed = false;
			renderer.material.mainTexture = wingsDown;

			if(velocity.y > -15)
			{
				this.rigidbody.AddForce(fall);
			}
		}
	}

	IEnumerator Wait()
	{
		waitActive = true;
		Debug.Log ("waiting...");
		yield return new WaitForSeconds(.1f);
		Debug.Log ("Done Waiting!");
		waitActive = false;
	}

}
