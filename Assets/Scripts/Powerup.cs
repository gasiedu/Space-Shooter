using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _PowerupSpeed = 3.0f;
    public GameObject prefab;
    //0 = triple shot
    //1 = Speed
    //2 = Sheilds
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        transform.Translate(Vector3.down * Time.deltaTime * _PowerupSpeed);

        //when we leave the screen, destroy this object
        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }

    }

    //ontriggercollison
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //communicate with the player script
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            {
             
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        player.ShieldPowerUpActive();
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
    
}
