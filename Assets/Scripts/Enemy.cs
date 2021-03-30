using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    public GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move the enemy down at 4m per second
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        //if bottom of screen
        if(transform.position.y < -5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is player
        if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        //destroy player
        //damage us

        //if other is laser
        if(other.gameObject.tag == "Player")
        { 
            Destroy(this.gameObject);
        }
        //destroy laser
        //destroy us
    }

}
