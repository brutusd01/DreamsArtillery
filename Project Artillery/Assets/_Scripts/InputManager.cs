using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]private PlayerInputs inputs;
    //Movement events
    //public delegate void moveAction();
    //public delegate void jumpAction();
    //public event jumpAction onJump;
    //public event moveAction onMove;
    //Gameplay events
 
    void OnEnable()
    {
        if(inputs == null)
        {
            inputs = new PlayerInputs();    
        }

        enableMoveInputs();
    }

    void OnDisable()
    {
        disableMoveInputs();
    }


    public void enableMoveInputs()
    {
        //THis will be moved to the playerscript in order to control movement. Ex: inputs.onJump += Jump; or inputs.onMove += GetInputs;
        inputs.Player.Jump.performed += JumpEvent;
        inputs.Player.Attack1.performed += Attack1Event;
        inputs.Player.Enable();
    }

    public void disableMoveInputs()
    {
        inputs.Player.Disable();
    }

    void Attack1Event(InputAction.CallbackContext ctx)
    {
        Debug.Log("Boom.");
    }
    void JumpEvent(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jumping!");
    }
    void FixedUpdate()
    {
        InputAction move = inputs.Player.Move;
        Debug.Log(move.ReadValue<Vector2>());
        
    }
}
