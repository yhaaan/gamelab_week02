using System;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject point1;
    public GameObject point2;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        lineRenderer.SetPosition(0, point1.transform.position);
        lineRenderer.SetPosition(1, point2.transform.position);
    }
}
