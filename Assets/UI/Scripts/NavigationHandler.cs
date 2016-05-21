using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavigationHandler : MonoBehaviour {
    public static Vector3 targetPosition = new Vector3(0,2,0);
    public static Text distanceDisplay; 
    private static MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        distanceDisplay = GameObject.Find("ui_distance").GetComponent<Text>();
    }

    void Update()
    {
        mr.enabled = true;

        distanceDisplay.text = (int)(Vector3.Distance(transform.position, targetPosition)*250) + "m";
        transform.LookAt(targetPosition);
        transform.Rotate(0, 180, 0);
    }
}
