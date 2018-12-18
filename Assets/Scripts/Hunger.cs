using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hunger : MonoBehaviour {

    [SerializeField]
    private float maxHunger;
    [SerializeField]
    private float hungerLossRate;
    [SerializeField]
    private string gameOverScene;
    [SerializeField]
    private string foodTag;

    public float hunger { get; private set; }
    public float MaxHunger { get { return maxHunger; } }



    private void Start()
    {
        hunger = maxHunger;
    }

    private void Update()
    {
        hunger -= hungerLossRate * Time.deltaTime;

        if (hunger <= 0)
            SceneManager.LoadScene(gameOverScene);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == foodTag)
        {
            Destroy(hit.gameObject);
            hunger = maxHunger;
        }
    }

}
