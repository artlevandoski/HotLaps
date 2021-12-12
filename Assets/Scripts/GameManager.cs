using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<CarController> cars = new List<CarController>();
    public Transform[] spawnPoints;
    public float positionUpdateRate = 0.05f;
    private float lastPositionUpdateTime;

    public int playersToBegin = 2;

    public int lapsToWin = 3;

    public bool gameStarted = false;
    public static GameManager instance; //this creates a singleton, so that this is able to be referred to anywhere in the project

    //use GameManager.* to access this script
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // used to update the car poistions on the track
        if(Time.time - lastPositionUpdateTime > positionUpdateRate)
        {
            lastPositionUpdateTime = Time.time;
            UpdateCarRacePositions();
        }

        // starts the game if car count is equal to players to begin 
        if(!gameStarted && cars.Count == playersToBegin)
        {
            gameStarted = true;
            StartCountdown();
        }

    }

    //begins the countdown display in PlayerUI once start countdown is triggered
    // looks for all objects = to Player UI and displays the countdown
    void StartCountdown()
    {
        PlayerUI[] uis = FindObjectsOfType<PlayerUI>();

        for(int x = 0; x < uis.Length; ++x)
            uis[x].StartCountdownDisplay();

        Invoke("BeginGame", 3.0f);
    }

    //once begin game is triggered this allows car control
    void BeginGame()
    {
        for(int x = 0; x < cars.Count; ++x)
        {
            cars[x].canControl = true;
        }
    }

    //updates the car positions
    void UpdateCarRacePositions()
    {
        cars.Sort(SortPosition);

        for(int x = 0; x < cars.Count; x++)
        {
            cars[x].racePosition = cars.Count - x;
        }
    }

    //determines car position based on zones passed
    int SortPosition (CarController a, CarController b)
    {
        if(a.zonesPassed > b.zonesPassed)
            return 1;
        else if(b.zonesPassed > a.zonesPassed)
            return -1;
        float aDist = Vector3.Distance(a.transform.position, a.curTrackZone.transform.position);
        float bDist = Vector3.Distance(a.transform.position, a.curTrackZone.transform.position);

        return aDist > bDist ? 1 : -1;

    }

    //determines winner after amount of laps (plus the first lap) is reached. Then runs the Game Over function
    public void CheckIsWinner (CarController car)
    {
        if(car.curLap == lapsToWin + 1)
        {
            for(int x = 0; x < cars.Count; ++x)
            {
                cars[x].canControl = false;
            }

            PlayerUI[] uis = FindObjectsOfType<PlayerUI>();

            for(int x = 0; x < uis.Length; ++x)
            uis[x].GameOver(uis[x].car == car);
        }
    }
}
