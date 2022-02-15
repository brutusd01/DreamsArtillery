using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManager", menuName = "Game/Input Manager")]
public class InputManager : ScriptableObject, PlayerInputs.IGameplayActions
//We don't need UIActions just yet.
//, PlayerInputs.IUIActions
{
    //Movement events
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<Vector2> lookEvent;
    public event UnityAction jumpEvent;
    public event UnityAction jumpCanceledEvent;
    //Gameplay events
    public event UnityAction attack1Event;
    //References
    private PlayerInputs inputs;


    private void OnEnable()
    {
        if(inputs == null)
        {
            inputs = new PlayerInputs(); 
            inputs.Gameplay.SetCallbacks(this);
            //We don't need UI yet!
            //inputs.UI.SetCallbacks(this);
        }
        enableMoveInputs();
    }

    private void OnDisable()
    {
        disableAllInputs();
    }

        public void OnMove(InputAction.CallbackContext context)
    {
        if(moveEvent != null)
        {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }
        public void OnLook(InputAction.CallbackContext context)
    {
        if(lookEvent != null)
        {
            lookEvent.Invoke(context.ReadValue<Vector2>());
        }
    }
        public void OnAttack1(InputAction.CallbackContext context)
    {
        if(attack1Event != null && context.phase == InputActionPhase.Performed)
        {
            attack1Event.Invoke();
        }
    }
        public void OnJump(InputAction.CallbackContext context)
    {
        if(jumpEvent != null && context.phase == InputActionPhase.Performed)
        {
            jumpEvent.Invoke();
        }
        if(jumpCanceledEvent != null && context.phase == InputActionPhase.Canceled)
        {
            jumpCanceledEvent.Invoke();
        }
    }

    public void enableMoveInputs()
    {
        //THis will be moved to the playerscript in order to control movement. Ex: inputs.onJump += Jump; or inputs.onMove += GetInputs;
        //inputs.Player.Jump.performed += JumpEvent;
       // inputs.Player.Attack1.performed += Attack1Event;
        inputs.Gameplay.Enable();
    }

    public void disableAllInputs()
    {
        inputs.Gameplay.Disable();
    }
    //No longer needed, all this script does is read inputs.
 /*   void FixedUpdate()
    {
        InputAction move = inputs.Player.Move;
        Debug.Log(move.ReadValue<Vector2>());
        
    }*/
}
