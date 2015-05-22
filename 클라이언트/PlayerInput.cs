using UnityEngine;
using System.Collections;

[AddComponentMenu("캐릭터/모든 스크립트")]
[RequireComponent(typeof(AdvancedMovement))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerInput : MonoBehaviour {


	void Start () {
	
	}
	
	void Update () {

        if (Input.GetButtonUp("Toggle Inventory"))
        {
           Messenger.Broadcast("ToggleInventory");
        }
        if (Input.GetButtonUp("Toggle BeadsBook"))
        {
            Messenger.Broadcast("ToggleBeadsBook");
        }


        if (Input.GetButtonUp("Toggle Character Window"))
        {
            Messenger.Broadcast("ToggleCharacterWindow");
        }

        if (Input.GetButton("Move Forward"))
        {
            if (Input.GetAxis("Move Forward") > 0)
            {
                SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);
            }
            else
            {
                SendMessage("MoveMeForward", AdvancedMovement.Forward.back);

            }
        }
        if (Input.GetButtonUp("Move Forward"))
        {
            SendMessage("MoveMeForward", AdvancedMovement.Forward.none);
        }




        if (Input.GetButton("Rotate Player"))
        {
            if (Input.GetAxis("Rotate Player") > 0)
            {
                SendMessage("RotateMe", AdvancedMovement.Turn.right);
            }
            else
            {
                SendMessage("RotateMe", AdvancedMovement.Turn.left);

            }
        }
        if (Input.GetButtonUp("Rotate Player"))
        {
            SendMessage("RotateMe", AdvancedMovement.Turn.none);
        }




        if (Input.GetButton("Strafe"))
        {
            if (Input.GetAxis("Strafe") > 0)
            {
                SendMessage("Strafe", AdvancedMovement.Turn.right);
            }
            else
            {
                SendMessage("Strafe", AdvancedMovement.Turn.left);

            }
        }

        if (Input.GetButtonUp("Strafe"))
        {
            SendMessage("Strafe", AdvancedMovement.Turn.none);
        }

        if (Input.GetButtonUp("Jump"))
        {
            SendMessage("JumpUp");
        }


        if (Input.GetButtonUp("Run"))
        {
            SendMessage("ToggleRun");  // 달리기 On/Off  
        }

	}
}
