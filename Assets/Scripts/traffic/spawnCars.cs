using UnityEngine;
using System.Collections;

public class spawnCars : MonoBehaviour
{
    public Transform spawnPath1;
    public Transform spawnPath2;
    public Transform spawnPath3;
    public Transform spawnPath4;
    public Transform spawnPath5;
    public Transform spawnPath6;
    public Transform spawnPath7;
    public Transform spawnPath8;
    public Transform spawnPath9;
    public Transform spawnPath10;
    public GameObject path1;
    public GameObject path2;
    public GameObject path3;
    public GameObject path4;
    public GameObject path5;
    public GameObject path6;
    public GameObject path7;
    public GameObject path8;
    public GameObject path9;
    public GameObject path10;
    public GameObject car;
    public int max;
    public float spawnTime;
    public int amountCars;


	// Use this for initialization
	void Start ()
    {
        amountCars = 0;
        InvokeRepeating("Spawning", spawnTime, spawnTime);
    }

	
	// Update is called once per frame
	void Update()
    {

	}

    void Spawning()
    {
        if (amountCars < max)
        {
            int i = Random.Range(1, 11);
            GameObject temp;
            if (i == 1)
            {
               temp = (GameObject)Instantiate(car, spawnPath1.position, spawnPath1.rotation);
                temp.GetComponent<carMove>().target = path1;
            }
            else if (i == 2)
            {
               temp = (GameObject)Instantiate(car, spawnPath2.position, spawnPath2.rotation);
                temp.GetComponent<carMove>().target = path2;
            }
            else if(i == 3)
            {
                temp = (GameObject)Instantiate(car, spawnPath3.position, spawnPath3.rotation);
                temp.GetComponent<carMove>().target = path3;
            }
            else if (i == 4)
            {
                temp = (GameObject)Instantiate(car, spawnPath4.position, spawnPath4.rotation);
                temp.GetComponent<carMove>().target = path4;
            }
            else if (i == 5)
            {
                temp = (GameObject)Instantiate(car, spawnPath5.position, spawnPath5.rotation);
                temp.GetComponent<carMove>().target = path5;
            }
            else if (i == 6)
            {
                temp = (GameObject)Instantiate(car, spawnPath6.position, spawnPath6.rotation);
                temp.GetComponent<carMove>().target = path6;
            }
            else if(i == 7)
            {
                temp = (GameObject)Instantiate(car, spawnPath7.position, spawnPath7.rotation);
                temp.GetComponent<carMove>().target = path7;
            }
            else if (i == 8)
            {
                temp = (GameObject)Instantiate(car, spawnPath8.position, spawnPath8.rotation);
                temp.GetComponent<carMove>().target = path8;
            }
            else if (i == 9)
            {
                temp = (GameObject)Instantiate(car, spawnPath9.position, spawnPath9.rotation);
                temp.GetComponent<carMove>().target = path9;
            }
            else
            {
                temp = (GameObject)Instantiate(car, spawnPath10.position, spawnPath10.rotation);
                temp.GetComponent<carMove>().target = path10;
            }

            temp.tag = "car";
            amountCars++;
        }
    }
}
