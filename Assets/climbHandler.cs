using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClimbHandler : MonoBehaviour
{
    
    [SerializeField] private GameObject controller; // Can be removed if not used
    public InputActionReference reset;

    void Start()
    {
       
        reset.action.performed += ResetScene; // Subscribe once in Start()
    }

    void OnDestroy()
    {
        reset.action.performed -= ResetScene; // Unsubscribe to prevent memory leaks
    }

    void ResetScene(InputAction.CallbackContext context)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("" + currentSceneName);
        switch (currentSceneName)
        {
            case "MainScene":
                SceneManager.LoadScene("MainScene2");
                break;
            case "MainScene2":
                SceneManager.LoadScene("MainScene3");
                break;
            case "MainScene3":
                SceneManager.LoadScene("MainScene");
                break;
            default:
                SceneManager.LoadScene("MainScene");
                break;
        }
    }

    
}
