using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    public GameObject prefab;
    [SerializeField]
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserprefab;
    private float _fireRate = 3.0f;
    private float _canfire = 3.0f;

    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The player is null");
        }
        _anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

      

        
    }

    void CalculateMovement()
    {
        //move the enemy down at 4m per second
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        //if bottom of screen
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Laser"))
        {
                        
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject, 2.6f);
            _enemySpeed = 0;
            
            Player player = other.transform.GetComponent<Player>();
            {
                player.AddScore(10);
            }
            
        }
                
        if(other.gameObject.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
            }
            _audioSource.Play();
            Destroy(this.gameObject, 2.6f);
            
        }
        
    }

}
