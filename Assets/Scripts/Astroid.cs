using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private int speed = 50;
    [SerializeField]
    private GameObject _AstroidAnim;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on the Z axis
        transform.Rotate(0,0, speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser")
        {
            Instantiate(_AstroidAnim, transform.localPosition, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
            
        }
            
}
    }
    //check for laser collision (trigger)
    //instantiate explosion at the position of the astroid(us)
    //destroy the explosion after 3 seconds



