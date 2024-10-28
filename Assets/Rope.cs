using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class CylinderBetweenPoints : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject cylinderPrefab; 
    [SerializeField] private float cylinderRadius = 0.02f; 
    [SerializeField]
    private InputActionReference ropeTrigger;
    [SerializeField] private float moveSpeed = 2f; // Speed of player movement along the cylinder
    [SerializeField] private GameObject player; // Reference to the player object
    [SerializeField] private LayerMask castLayer; // LayerMask to filter raycast targets

     private GameObject activeCylinder; 
    private Vector3 cylinderEndPoint; 
    private bool isMoving = false;
    void Start()
    {
        ropeTrigger.action.performed += DoRaycast;
    }
    void Update()
    {
        // Move the player along the cylinder if movement is active
        if (isMoving)
        {
            MovePlayerAlongCylinder();
        }
    }
    void DoRaycast(InputAction.CallbackContext _)
    {
        // Check if the trigger is pressed
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, castLayer)) {
            CreateOrUpdateCylinder(controller.transform.position, hit.point);
            // Set the endpoint and enable movement
            cylinderEndPoint = hit.point;
            isMoving = true; // Enable movement

        }
    }

    private void CreateOrUpdateCylinder(Vector3 startPoint, Vector3 endPoint)
    {
       
        activeCylinder = Instantiate(cylinderPrefab);
        

        // Calculate the midpoint and direction between the two points
        Vector3 direction = endPoint - startPoint;
        Vector3 midpoint = startPoint + (direction / 2);

        // Set the position and scale of the cylinder
        activeCylinder.transform.position = midpoint;
        activeCylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        activeCylinder.transform.localScale = new Vector3(cylinderRadius, direction.magnitude / 2, cylinderRadius);
    }
    private void MovePlayerAlongCylinder()
    {
        
        player.transform.position = Vector3.MoveTowards(player.transform.position, cylinderEndPoint, moveSpeed * Time.deltaTime);

        
        if (Vector3.Distance(player.transform.position, cylinderEndPoint) < 0.01f)
        {
            isMoving = false;
        }
    }
}
