using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
  public bool isGate;
  private void OnTriggerEnter (Collider other)
  {
      if(other.CompareTag("Player"))
      {
          CarController car = other.GetComponent<CarController>();
          car.curTrackZone = this;
          car.zonesPassed++; // tracks the number of zones passed by each car to track position

          if(isGate)
          {
            car.curLap++;
            GameManager.instance.CheckIsWinner(car); //checks which car is the winner of the race
          }
      }
  }
}
