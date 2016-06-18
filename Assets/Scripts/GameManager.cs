using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public State state;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}

	}

	public enum State
	{
		Game = 0,
		Goal = 1,

	}

	// Use this for initialization
	void Start () {
		state = State.Game;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToGoal(){
		state = State.Goal;
		GameObject.Find ("Canvas").GetComponent<Animator> ().SetTrigger ("Goal");
	}

	public void ToGame(){
		state = State.Game;
		GameObject.FindGameObjectWithTag ("Player").SendMessage ("ToStartPos");
	}
}
