using UnityEngine;
using System.Collections;

public class collisionAvoidance : MonoBehaviour {

    private float distance;
    private bool isStopped;
    private Transform onCollision;
    private bool isDeleted;
    private Color temp;

    private float lastCollisionWithPlayer;

	// Use this for initialization
	void Start ()
    {
        distance =0.2f;
        isStopped = false;
        isDeleted = false;
        temp = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += 0.01f;
        Debug.DrawRay(position, transform.forward, temp);

        if (Physics.Raycast(position, transform.forward, out hit, distance))
        {
            Debug.Log("I hit something");
            if (hit.collider.tag.Equals("car"))
            {
                if (!hit.collider.gameObject.GetComponent<collisionAvoidance>().isStopped)
                {
                    Debug.Log("Ray hit a car");
                    this.GetComponent<NavMeshAgent>().Stop();
                    isStopped = true;
                }
            }

            if(hit.collider.tag.Equals("Player"))
            {
                Debug.Log("Ray hit the player");
                this.GetComponent<NavMeshAgent>().Stop();
                isStopped = true;
            }
        }
        else
        {
            if (isStopped)
            {
                this.GetComponent<NavMeshAgent>().Resume();
                isStopped = false;
            }
        }
    }

   void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("car")  && !collision.gameObject.GetComponent<collisionAvoidance>().isStopped)
            {
            onCollision = this.transform;
            this.GetComponent<NavMeshAgent>().Stop();
            isStopped = true;
         }

        if (tag.Equals("Player"))
        {
            onCollision = this.transform;
            this.GetComponent<NavMeshAgent>().Stop();
            isStopped = true;

            if((Time.time - lastCollisionWithPlayer) > 5.0f)
            {
                if(!GameOverOverlay.GameIsOver)
                    CashEarnedHandler.notifyCashLost(50);

                lastCollisionWithPlayer = Time.time;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("car"))
        {
            if (collision.gameObject.GetComponent<collisionAvoidance>().isDeleted)
            {
                temp=Color.green;
                this.GetComponent<NavMeshAgent>().Resume();
                isStopped = false;
            }
            else
            {
                Debug.Log("car entering delete mode");
                isDeleted = true;
                GameObject.Find("Fukuoka_Ground").GetComponent<spawnCars>().amountCars -= 1;
                DestroyObject(this.gameObject);
                Debug.Log("car deleted");
            }
        }
    }

}
