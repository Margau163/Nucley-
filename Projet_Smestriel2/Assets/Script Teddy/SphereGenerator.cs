using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int sphereQuantity = 50;
    [SerializeField] private float radius = 25f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpheres();
    }

    private void GenerateSpheres()
    {
        var parent = GameObject.Find("CLine");

        for (int i = 0; i < sphereQuantity; i++)
        {
            GameObject sphere = Instantiate(prefab, transform);
            sphere.transform.position = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
        }
    }
}
