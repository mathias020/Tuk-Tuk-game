using UnityEngine;
using System.Collections;

public class OptionButtonHandler : MonoBehaviour {
    public void performAction()
    {
        Debug.Log("You selected: " + gameObject.name);
    }
}
