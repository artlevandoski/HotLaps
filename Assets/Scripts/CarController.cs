using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
   public float acceleration;
   public float turnSpeed;

   public Transform carModel;
   private Vector3 startModelOffset;

   public float groundCheckRate;
   private float lastGroundCheckTime;

   private float curYrot;

   private bool accelerateInput;
   private float turnInput;

   public Rigidbody rig;

   void Start ()
   {
       startModelOffset = carModel.transform.localPosition; //local position of the car model
   }

   //Update runs at the games fps
   void Update ()
   {
       curYrot += turnInput * turnSpeed * Time.deltaTime; //uses Y axis to turn the car

       carModel.position = transform.position + startModelOffset; //sets car model position
       carModel.eulerAngles = new Vector3(0, curYrot, 0); //sets rotation to not rotate with the car
   }

   //Fixed Update always runs at 60 times per second, this is good for physics calculations which unity uses
   void FixedUpdate ()
   {
       if(accelerateInput == true)
       {
           rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration); //begins pushing the car model forward ignoring mass
       }
   }

   public void OnAccelerateInput (InputAction.CallbackContext context) //used to determine if accelerate button is pushed down or not
   {
        if(context.phase == InputActionPhase.Performed)
            accelerateInput = true;
        else 
            accelerateInput = false;
   }

   public void OnTurnInput (InputAction.CallbackContext context) //used to determine the variable of the turning negative or postive

        {
            turnInput = context.ReadValue<float>();
        }





}
