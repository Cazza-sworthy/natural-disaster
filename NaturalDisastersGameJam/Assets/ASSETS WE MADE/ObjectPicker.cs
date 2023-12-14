using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ObjectPicker : MonoBehaviour
{
    public float hoverDistance = 2f; // Distance to hover in front of the camera
    private bool isObjectPickedUp = false;
    private GameObject pickedObject;
    private Vector3 offset; // Offset between the object's position and the mouse position
 
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (!isObjectPickedUp)
            {
                // Try to pick up an object
                PickUpObject();
            }
            else
            {
                // Drop the currently picked up object
                DropObject();
            }
        }
 
        // If an object is picked up, move it with a smooth position in front of the camera
        if (isObjectPickedUp)
        {
            MovePickedObject();
        }
    }
 
    void PickUpObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
 
        // Check if the ray hits a game object
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a rigidbody (physics)
            if (hit.collider != null && hit.collider.GetComponent<Rigidbody>() != null)
            {
                // Pick up the object
                pickedObject = hit.collider.gameObject;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;
 
                // Calculate the offset between the object's position and the camera position
                offset = pickedObject.transform.position - (Camera.main.transform.position + Camera.main.transform.forward * hoverDistance);
 
                isObjectPickedUp = true;
            }
        }
    }
 
    void DropObject()
    {
        // Drop the picked up object
        if (pickedObject != null)
        {
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject = null;
            isObjectPickedUp = false;
        }
    }
 
    void MovePickedObject()
    {
        if (pickedObject != null)
        {
            // Move the picked up object with a smooth position in front of the camera
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * hoverDistance;
            pickedObject.GetComponent<Rigidbody>().MovePosition(targetPosition + offset);
        }
    }
}