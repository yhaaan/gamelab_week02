using System;
using System.Collections;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public float speed;


   

    private void Update()
    {
        transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            if(transform.localPosition.x <= -6)
                transform.localPosition = new Vector3(6, -0.5f, 0);
        
    }
}
