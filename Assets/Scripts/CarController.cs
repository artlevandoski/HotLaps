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

   public bool canControl;

   private bool accelerateInput;
   private float turnInput;

   public TrackZone curTrackZone;
   public int zonesPassed;
   public int racePosition;
   public int curLap;


   public Rigidbody rig;

   void Start ()
   {
       startModelOffset = carModel.transform.localPosition; //local position of the car model
       GameManager.instance.cars.Add(this);
       transform.position = GameManager.instance.spawnPoints[GameManager.instance.cars.Count - 1].position;
   }

   //Update runs at the games fps
   void Update ()
   {
       if(!canControl)
        return;

       float turnRate = Vector3.Dot(rig.velocity.normalized, carModel.forward); //determines the difficulty of turning
       turnRate = Mathf.Abs(turnRate); //returns an absolute value on a negative axis

       curYrot += turnInput * turnSpeed * turnRate * Time.deltaTime; //uses Y axis to turn the car

       carModel.position = transform.position + startModelOffset; //sets car model position
       //carModel.eulerAngles = new Vector3(0, curYrot, 0); //sets rotation to not rotate with the car

       CheckGround();//calls check ground function to determine the cars rotation
   }

   //Fixed Update always runs at 60 times per second, this is good for physics calculations which unity uses
   void FixedUpdate ()
   {
       if(!canControl)
        return;

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

   void CheckGround()
   {
       //sets a point of origin from middle of sphere to ground and looks ahead to orient the rotation of the car model
       Ray ray = new Ray (transform.position + new Vector3(0, -0.75f,0), Vector3.down);
       RaycastHit hit;

       if(Physics.Raycast(ray, out hit, 1.0f))
       {
           carModel.up = hit.normal;
       }
       else
       {
           carModel.up = Vector3.up;
       }

       carModel.Rotate(new Vector3(0, curYrot, 0), Space.Self); //maintains the same Y axis regardless of X axis
   }

   public void OnTurnInput (InputAction.CallbackContext context) //used to determine the variable of the turning negative or postive

        {
            turnInput = context.ReadValue<float>();
        }





}
