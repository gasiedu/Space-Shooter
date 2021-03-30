using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
        private int LaserSpeed = 8;
    
    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.up * LaserSpeed * Time.deltaTime);
            
        if (transform.position.y > 8)
        { Destroy(gameObject); }

     }
}
