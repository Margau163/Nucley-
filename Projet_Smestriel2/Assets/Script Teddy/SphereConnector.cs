using System.Collections.Generic;
using UnityEngine;

public class SphereConnector : MonoBehaviour
{
    [SerializeField] private float connectionRadius = 5f; // Adjust this radius as needed
    [SerializeField] private string objectType = "YourObjectType"; // Set the object type in the Unity Editor
    [SerializeField] private int minDistance = 2; // Set the minimum distance between objects

    public List<GameObject> connections = new List<GameObject>();

    private List<GameObject> nearestRoot = new List<GameObject>();

    [SerializeField] private bool isRootNeuron = false;
    

    void Start()
    {
        //CheckCollisions();
        ConnectWithNearestObjects();
        CheckConnections();
        DrawConnections(connections);
    }

    void ConnectWithNearestObjects()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(objectType);

        List<GameObject> nearestObjects = new List<GameObject>();

        // Find the nearest objects of the same type
        foreach (GameObject obj in allObjects)
        {
            if (obj != gameObject /*&& (nearestObjects.Count<maxConnections)*/)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < connectionRadius && (distance > minDistance))
                {
                    nearestObjects.Add(obj);
                }
            }
        }

        // Connect with the nearest objects
        connections = nearestObjects;


    }

    void DrawConnections(List<GameObject> connections)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(objectType);
        foreach (GameObject objectToCheck in connections)
        {
            if (objectToCheck)
            {
                // Add your connection logic here, for example, you can create a line between objects
                ConnectObjects(objectToCheck);
            }

            if (objectToCheck.gameObject.GetComponent<SphereConnector>().isRootNeuron)
            {
                objectToCheck.tag = "rootNeuron";
            }
        }

        /*GameObject[] allRoots = GameObject.FindGameObjectsWithTag("rootNeuron");

        foreach (GameObject rootToCheck in allRoots)
        {
            float distance = Vector3.Distance(transform.position, rootToCheck.transform.position);

            if (distance < connectionRadius*2 && (distance > minDistance))
            {
                connections.Add(rootToCheck);
                // Add your connection logic here, for example, you can create a line between objects
                ConnectObjects(rootToCheck);
            }
        }*/
    }

    void ConnectObjects(GameObject otherObject)
    {
        // Add your connection logic here
        // For example, you can create a line or use other Unity components
        // For demonstration, let's create a debug line
        Debug.DrawLine(transform.position, otherObject.transform.position, Color.blue, Mathf.Infinity);
    }

    void CheckCollisions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f); // Adjust the radius as needed

        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to a different object
            if (collider.gameObject != gameObject)
            {
                // If there's a collision, destroy the object
                Destroy(gameObject);
                break; // Exit the loop since the object is already destroyed
            }
        }
    }

    void CheckConnections()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(objectType);

        foreach (GameObject obj in allObjects)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i] == null)
                {
                    connections[i] = gameObject;
                }
            }
            if (connections.Count == 0)
            {
                Destroy(gameObject);
            }

            if (connections.Count == 1)
            {
                //gameObject.tag = "rootNeuron";
                isRootNeuron = true;
            }
        }

        /*GameObject[] allRoots = GameObject.FindGameObjectsWithTag("rootNeuron");

        foreach (GameObject root in allRoots)
        {
            if (root != gameObject)
            {
                float distance = Vector3.Distance(transform.position, root.transform.position);

                if (distance < connectionRadius*1.3)
                {
                    nearestRoot.Add(root);
                }
            }
        }

        foreach (GameObject root in nearestRoot)
        {
            if (root)
            {
                // Add your connection logic here, for example, you can create a line between objects
                ConnectObjects(root);
            }

        }*/

    }

}
