using UnityEngine;
using System.Collections;

public class playerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(!collision.gameObject.tag.Equals("car"))
        {
            Debug.Log("I hit something - Player");
        }
    }
}
