using UnityEngine;
using System.Collections;

public class carMove : MonoBehaviour {

    public GameObject target;
    public float timeToChange;
    public bool isStopped;

	// Use this for initialization
	void Start ()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

        if (agent.remainingDistance<=timeToChange)
        {
            if (target.name.Equals("decision"))
            {
                int random = Random.Range(1, 3);

                if(random%2==0)
                {
                    target = target.GetComponent<nextWayPoint>().turn;
                }
                else
                    target = target.GetComponent<nextWayPoint>().next;
            }
            else if(target.name.Equals("finish"))
            {
                GameObject spawn = GameObject.Find("spawnPath8");
                GetComponent<NavMeshAgent>().Stop();
                this.GetComponent<NavMeshAgent>().Warp(spawn.transform.position);
                this.transform.rotation = spawn.transform.rotation;
                GetComponent<NavMeshAgent>().Resume();
                target = target.GetComponent<nextWayPoint>().next;
            }
            else if (target.name.Equals("finish2"))
            {
                GameObject spawn = GameObject.Find("spawnPath5");
                GetComponent<NavMeshAgent>().Stop();
                this.GetComponent<NavMeshAgent>().Warp(spawn.transform.position);
                this.transform.rotation = spawn.transform.rotation;
                GetComponent<NavMeshAgent>().Resume();
                target = target.GetComponent<nextWayPoint>().next;
            }
            else
            target = target.GetComponent<nextWayPoint>().next;
        }

        agent.SetDestination(target.transform.position);
    }
}
