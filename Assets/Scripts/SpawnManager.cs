using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    

    private bool _stopSpawning = false;
        
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        while (_stopSpawning == false)
        {
            //instantiate enemy prefab
            float randomX = Random.Range(-9f, 9f);
            GameObject newEnemy = Instantiate(_enemyprefab, new Vector3(randomX, 7, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //yield wait for 5 seconds
            yield return new WaitForSeconds(5.0f);

        }
       
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            float poweruptimer = Random.Range(3, 7);
            float randomX = Random.Range(-9f, 9f);
            int randomPowerUP = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUP], new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(poweruptimer);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    //spawn gameobjects every 5 seconds
    //create a coroutine of type IEnumertor -- yield event
    // while loop
}
