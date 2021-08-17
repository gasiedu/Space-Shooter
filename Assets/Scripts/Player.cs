using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.5f;
    [SerializeField]
    private float _speedmultiplier = 2.0f;
    public float horizontalInput;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _firerate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    private GameObject _thruster;
    private GameObject _afterburner;
    private bool _afterburnersOn = false;

    [SerializeField]
    private GameObject _Tripleshotprefab;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedPowerupActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _ShieldVisual;

    [SerializeField]
    private GameObject _rightengine;
    [SerializeField]
    private GameObject _leftengine;
    private BoxCollider2D _2DCollider;

    private UIManager _uIManager;

    [SerializeField]
    private int _Score;

    [SerializeField]
    private AudioClip _laserAudio;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _2DCollider = GetComponent<BoxCollider2D>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is null");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }

        _thruster = gameObject.transform.Find("Thruster").gameObject;
        if (_thruster == null)
        {
            Debug.LogError("Thruster is nuill in Player");
        }

        _afterburner = gameObject.transform.Find("Afterburner").gameObject;
        if (_afterburner == null)
        {
            Debug.LogError("After Burner is null in player");
        }


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_afterburnersOn == false)
            {
                _speed *= 2f;
                _afterburner.SetActive(true);
                _thruster.SetActive(false);
                _afterburnersOn = true;
            }
        }
        else
        {
            if (_afterburnersOn == true)
            {
                _speed /= 2f;
                _afterburnersOn = false;
                _afterburner.SetActive(false);
                _thruster.SetActive(true);
            }
        }
       

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y > 3.8f)
        {
            transform.position = new Vector3(transform.position.x, 3.8f, 0);
        }
        else if (transform.position.y < -3.8f)
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

       void FireLaser ()
    {
        _canFire = Time.time + _firerate;

        if(_isTripleShotActive == true)
        {
           
            Instantiate(_Tripleshotprefab, transform.position + new Vector3(-0.8f, 0.2f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        AudioSource.PlayClipAtPoint(_laserAudio, transform.position);

    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _ShieldVisual.SetActive(false);
            return;
        }
        _lives -= 1;

        if (_lives == 2)
        {
            _rightengine.SetActive(true);
            StartCoroutine(Invulnarability());
        }
        else if (_lives ==1)
        {
            _leftengine.SetActive(true);
            StartCoroutine(Invulnarability());
        }
        
        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            //communicate with spawn manager 
            _spawnManager.onPlayerDeath();
            //let them know to stop spawn manager
            Destroy(this.gameObject);
            
        }
    }

    public void TripleShotActive() 
    {
        //tripleshot active becomes true
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    //start coroutine power down for tripleshot
    }
        
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedPowerupActive()
    {       
            StartCoroutine(SpeedBoostPowerDown());          
        
    }

    IEnumerator SpeedBoostPowerDown()
    {
        _speed = _speedmultiplier;
        yield return new WaitForSeconds(5.0f);
        _speed = 7.5f;        
    }

    public void ShieldPowerUpActive()
    {
        _isShieldActive = true;
        _ShieldVisual.SetActive(true);
    }

    public void AddScore(int points)
    {
        _Score += points;
        _uIManager.UpdateScore(_Score);
    }
    
  
    IEnumerator Invulnarability()
    {
        _2DCollider.enabled = false;
        yield return new WaitForSeconds(3.0f);
        _2DCollider.enabled = true;
        Debug.Log("Invulnablity running");
    }
}
