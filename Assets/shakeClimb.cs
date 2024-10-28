using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeClimb : MonoBehaviour
{
    [SerializeField] private float threshold = 5f;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject wall;

    private Vector3 rightPos;
    private Vector3 leftPos;

    void Start()
    {
        // Store initial hand positions
        rightPos = rightHand.transform.position;
        leftPos = leftHand.transform.position;
    }

    void Update()
    {

        Vector3 rightVelocity = (rightHand.transform.position - rightPos) / Time.deltaTime;
        Vector3 leftVelocity = (leftHand.transform.position - leftPos) / Time.deltaTime;


        if (
            rightVelocity.y >= threshold && leftVelocity.y >= threshold)
        {
       
            head.transform.position += Vector3.up * speed *Time.deltaTime;
        }

        // Update previous positions for the next frame
        rightPos = rightHand.transform.position;
        leftPos = leftHand.transform.position;
    }
}
