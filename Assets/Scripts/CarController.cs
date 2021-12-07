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

   public void OnAccelerateInput (InputAction.CallbackContext context)
   {
        if(context.phase == InputActionPhase.Performed)
            accelerateInput = true;
        else 
            accelerateInput = false;
   }

   public void OnTurnInput (InputAction.CallbackContext context)

        {
            turnInput = context.ReadValue<float>();
        }





}
