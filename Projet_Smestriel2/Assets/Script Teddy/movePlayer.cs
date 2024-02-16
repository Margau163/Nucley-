using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] private Transform objectToMove; // Assign the object you want to move in the Inspector

    public List<GameObject> connexion = null;
    private List<GameObject> nearestObjects = null;
    

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has a collider
                if (hit.collider != null && ((nearestObjects == null)||(nearestObjects.Contains(hit.collider.gameObject))))
                {
                    // Move the objectToMove to the position where the click occurred
                    MoveObjectToPosition(objectToMove, hit.point);
                    nearestObjects = hit.collider.GetComponent<SphereConnector>().connections;
                }
            }
        }
    }

    void MoveObjectToPosition(Transform obj, Vector3 position)
    {
        // Move the object to the clicked position
        obj.position = position;
    }
}
