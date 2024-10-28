using UnityEngine;
using UnityEngine.InputSystem;


public class Pull : MonoBehaviour
{
    [SerializeField] private GameObject controller; // Reference to XR controller
    [SerializeField] private InputActionReference triggerAction; // Trigger input reference
    [SerializeField] private float moveSpeed = 2f; // Speed of upward movement
    [SerializeField] private GameObject player; // Reference to the player object
    [SerializeField] private string climbableTag = "Climbable"; // Tag for climbable objects

    private bool isTouchingClimbable = false; // Tracks if the controller is touching a climbable object
    private Vector3 lastControllerPosition; // Stores the controller's last position
    private bool climbing = false;
    private void Start()
    {
       
        triggerAction.action.started += OnTriggerPressed;
        triggerAction.action.canceled += OnTriggerReleased;
    }

    

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (isTouchingClimbable)
        {
            climbing = true;
            // Store the initial controller position when the trigger is pressed
            lastControllerPosition = controller.transform.position;
        }
    }

    private void OnTriggerReleased(InputAction.CallbackContext context)
    {
        // Reset the controller position tracking when the trigger is released
        lastControllerPosition = Vector3.zero;
        climbing = false;
    }

    private void Update()
    {
        if (triggerAction.action.IsPressed() && climbing)
        {
            // Calculate the downward movement of the controller
            Vector3 controllerMovement = lastControllerPosition - controller.transform.position;

            // If the controller moves downward, move the player upward
            if (controllerMovement.y > 0)
            {
                MovePlayerUp(controllerMovement.y);
            }

            // Update the last known position of the controller
            lastControllerPosition = controller.transform.position;
        }
    }

    private void MovePlayerUp(float amount)
    {
        // Move the player upward based on the downward controller movement
        player.transform.position += Vector3.up * amount * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // Check if the controller is touching a climbable object
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        // Check if the controller is touching a climbable object
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the flag when the controller stops touching a climbable object
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = false;
        }
    }
}
