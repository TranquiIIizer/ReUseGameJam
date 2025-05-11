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
    private float _speedModifier = 1;

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


            Quaternion spawnRotation = _spawnPoints[spawnPointIndex].rotation;
            spawnRotation = Quaternion.Euler(110f, spawnRotation.eulerAngles.y, spawnRotation.eulerAngles.z);

            GameObject vehicle = Instantiate(_vehiclePrefabs[vehicleIndex], _spawnPoints[spawnPointIndex].position, spawnRotation);

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
        return _secondsBetweenEachSpawn > 5 ? _secondsBetweenEachSpawn -= (float)_spawnCount/10 : _secondsBetweenEachSpawn;
    }

    private float IncreaseVehicleSpeed()
    {
        return _speedModifier <= 2.5 ? _speedModifier += (_spawnCount / 10) : _speedModifier;
    }
}
