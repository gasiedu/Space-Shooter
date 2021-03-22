using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private referance
    //data type (int, float, bool, strings)
    //everu vaiable has a name
    //optional value assigned

    private float _speed = 5.5f;

    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 3.8f, 0);
        }
        else if (transform.position.y < 3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.9f)
        {
            transform.position = new Vector3(-11.9f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.9f)
        {
            transform.position = new Vector3(11.9f, transform.position.y, 0);
        }
    }
}
