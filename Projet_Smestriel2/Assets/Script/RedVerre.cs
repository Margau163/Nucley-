using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerreRouge : MonoBehaviour
{
    private int currentPointIndex = 0;
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, points[currentPointIndex].position) < 0.1f)
            {
                // Choisit un index aléatoire différent du point actuel
                int previousPointIndex = currentPointIndex;
                do
                {
                    currentPointIndex = UnityEngine.Random.Range(0, points.Length);
                } 
                while (currentPointIndex == previousPointIndex);
            }
            transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

            yield return null;
        }
    }

}
