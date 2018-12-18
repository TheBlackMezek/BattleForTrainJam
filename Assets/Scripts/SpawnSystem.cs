using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform[] spawns;
    [SerializeField]
    private float minSpawnInterval;
    [SerializeField]
    private float maxSpawnInterval;
    [SerializeField]
    private Transform hunter;

    private float timer;



    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
            GameObject prey = Instantiate(prefab);
            prey.transform.position = spawns[Random.Range(0, spawns.Length)].position;
            prey.transform.eulerAngles = Vector3.up * Random.Range(0f, 360f);
            prey.GetComponent<PreyController>().hunter = hunter;
        }
    }

    public void TeleMouse(Transform mouse)
    {
        mouse.position = spawns[Random.Range(0, spawns.Length)].position;
        mouse.eulerAngles = Vector3.up * Random.Range(0f, 360f);
    }

}
