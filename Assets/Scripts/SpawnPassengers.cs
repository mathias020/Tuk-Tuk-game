using UnityEngine;
using System.Collections;

public class SpawnPassengers : MonoBehaviour {

	public GameObject human;
	public Transform target;

	// Use this for initialization
	void Start () {
		int positionX;
		int positionZ;
		for (int i = 0; i < 5; i++)
		{
			positionX = Random.Range(0, 100);
			positionZ = Random.Range(0, 100);

			Instantiate(human, new Vector3(positionX, 2, positionZ), Quaternion.Euler(0, 0, 0));
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}