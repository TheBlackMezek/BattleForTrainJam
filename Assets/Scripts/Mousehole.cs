using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousehole : MonoBehaviour {

    [SerializeField]
    private SpawnSystem spawner;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.gameObject.GetComponent<PreyController>())
        {
            spawner.TeleMouse(other.transform);
        }
    }

}
