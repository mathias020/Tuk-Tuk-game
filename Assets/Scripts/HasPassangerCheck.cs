using UnityEngine;
using System.Collections;

public class HasPassangerCheck : MonoBehaviour {
	private bool hasPassanger;

	public bool getPassangerState(){
		return this.hasPassanger;
	}
	public void setPassangerState(bool input){
		this.hasPassanger = input;
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
