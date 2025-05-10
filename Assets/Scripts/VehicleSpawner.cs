using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _vehiclePrefabs = new();
    [SerializeField] private List<Transform> _spawnPoints = new();

    private int _spawnCount = 0;
    [SerializeField] private float _secondsBetweenEachSpawn;
    [SerializeField] private float _spawnRateModifier;
    [Header("A value which vehicle movement speed is multiplied by")]
    [SerializeField] private float _speedModifier;

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    IEnumerator SpawnVehicle()
    {
        while (true) 
        {
            yield return new WaitForSeconds(_secondsBetweenEachSpawn);

            int vehicleIndex = Random.Range(0, _vehiclePrefabs.Count);
            int spawnPointIndex = Random.Range(0, _spawnPoints.Count);

            GameObject vehicle = Instantiate(_vehiclePrefabs[vehicleIndex], _spawnPoints[spawnPointIndex], true);
            vehicle.GetComponent<VehicleMovement>().movementSpeed *= _speedModifier;

            _spawnCount++;

            if (_spawnCount < 20)
            {
                IncreaseSpawnRate();
                IncreaseVehicleSpeed();
            }
        }
    }

    private float IncreaseSpawnRate()
    {
        return _secondsBetweenEachSpawn -= _spawnCount;
    }

    private float IncreaseVehicleSpeed()
    {
        return _speedModifier += (_spawnCount*2)/10;
    }
}
