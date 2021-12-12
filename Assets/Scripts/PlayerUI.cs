using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI carPositionText;
    public CarController car;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI gameOverText;

    void Update()
        {
            //tracks car position and changed the interger to a string to display in car position text
            carPositionText.text = car.racePosition.ToString() + " / " + GameManager.instance.cars.Count.ToString();
        }

    public void StartCountdownDisplay()
    {
       StartCoroutine (CountDown()); 

       IEnumerator CountDown() //begins the countdown timer, from 3, pausing the function for 1 second between each
       {
           countdownText.gameObject.SetActive(true);
           countdownText.text = "3";
           yield return new WaitForSeconds(1.0f);
           countdownText.text = "2";
           yield return new WaitForSeconds(1.0f);
           countdownText.text = "1";
           yield return new WaitForSeconds(1.0f);
           countdownText.text = "GO!";
           yield return new WaitForSeconds(1.0f);
           countdownText.gameObject.SetActive(false);


       } 
    }

    //function that changes the game winner text color and winner for gamer over text
    public void GameOver (bool Winner)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.color = Winner == true? Color.green : Color.red;
        gameOverText.text = Winner == true? "You Win" : "You Lost";
    }


}
