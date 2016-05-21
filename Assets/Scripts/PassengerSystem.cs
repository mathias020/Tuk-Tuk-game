using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PassengerSystem : MonoBehaviour {

    private static int passengerNumber = -1;
    public GameObject passengerPrefab;

    private float timeFromWin;

    public static int PassengerIndex
    {
        get { return passengerNumber; }
        set { passengerNumber = value; }
    }

    public static Vector3 NextPassengerPosition
    {
        get {
            if ((passengerNumber + 1) >= 0 && (passengerNumber + 1) < instance.passengers.Length)
                return instance.passengers[passengerNumber + 1].spawnPosition.position;
            else
                return new Vector3(0, 0, 0);
        }
    }

    public static Vector3 NextPassengerDestination
    {
        get
        {
            if ((passengerNumber + 1) >= 0 && (passengerNumber + 1) < instance.passengers.Length)
                return instance.passengers[passengerNumber + 1].destination.position;
            else
                return new Vector3(0, 0, 0);
        }
    }

    public static Vector3 CurrentPassengerDestination
    {
        get {
            if (passengerNumber >= 0 && passengerNumber < instance.passengers.Length)
                return instance.passengers[passengerNumber].destination.position;
            else
                return new Vector3(0, 0, 0);
        }
    }

    [Serializable]
    public struct Passenger
    {
        public Transform spawnPosition;
        public Transform destination;
    }

    public Passenger[] passengers;

    private static PassengerSystem instance;

	void Start () {
        instance = this;
        Debug.Log("Passengers: " + passengers.Length);
        for (int i = 0; i < passengers.Length; i++)
            newHuman(passengers[i].spawnPosition);
	}

    private void newHuman(Transform t)
    {
        Debug.Log("Created human");
        GameObject newHuman = Instantiate(passengerPrefab);
        newHuman.transform.position = t.position;
        newHuman.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if(!DropOff.hasPassanger && passengerNumber == -1 && !NavigationHandler.targetPosition.Equals(NextPassengerPosition))
        {
            NavigationHandler.targetPosition = NextPassengerPosition;
        }

        if(passengerNumber == passengers.Length-1)
        {
            // Game Over
            if(!GameOverOverlay.GameIsOver)
            {
                GameOverOverlay.Score = PointSystem.score;
                GameOverOverlay.GameIsOver = true;

                timeFromWin = Time.time;
            }
            else if(Time.time - timeFromWin > InputConstants.MENU_ACTION_DELAY && !LoadingOverlayHandler.IsLoading && (Input.GetKeyDown(KeyCode.E)) )
            {
                Debug.Log("Going to Main Menu");
                LoadingOverlayHandler.LoadNewScene(0);
            }
        }
    }
}
