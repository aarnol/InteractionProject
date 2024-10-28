using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbHandler : MonoBehaviour
{
    public GameObject playerOrigin;
    private Vector3 initialPosition;
    private int currentScript = 0; // Ensure it's properly managed elsewhere.

    [SerializeField] private GameObject controller; // Can be removed if not used
    public InputActionReference reset;

    void Start()
    {
        initialPosition = playerOrigin.transform.position;
        reset.action.performed += ResetScene; // Subscribe once in Start()
    }

    void OnDestroy()
    {
        reset.action.performed -= ResetScene; // Unsubscribe to prevent memory leaks
    }

    void ResetScene(InputAction.CallbackContext context)
    {
        // Find and destroy all rope objects
        GameObject[] ropeObjects = GameObject.FindGameObjectsWithTag("Rope");
        foreach (GameObject rope in ropeObjects)
        {
            Destroy(rope);
        }

        // Reset player position
        playerOrigin.transform.position = initialPosition;

        // Switch between climbing-related scripts based on currentScript
        switch (currentScript)
        {
            case 0:
                EnableScript<shakeClimb>(false);
                EnableScript<CylinderBetweenPoints>(true);
                break;
            case 1:
                EnableScript<CylinderBetweenPoints>(false);
                EnableScript<Pull>(true);
                break;
            default:
                EnableScript<Pull>(false);
                EnableScript<shakeClimb>(true);
                break;
        }
    }

    void EnableScript<T>(bool enable) where T : MonoBehaviour
    {
        var script = gameObject.GetComponent<T>();
        if (script != null)
        {
            script.enabled = enable;
        }
    }
}
