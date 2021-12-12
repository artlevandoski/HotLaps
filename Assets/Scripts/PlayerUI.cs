using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI carPositionText;
    public CarController car;
    public TextMeshProUGUI countdownText;

    void Update()
        {
            carPositionText.text = car.racePosition.ToString() + " / " + GameManager.instance.cars.Count.ToString();
        }

    public void StartCountdownDisplay()
    {
       StartCoroutine (CountDown()); 

       IEnumerator CountDown()
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


}
