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

    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float minPitch;
    [SerializeField]
    private float maxPitch;
    [SerializeField]
    private float minMeowWait;
    [SerializeField]
    private float maxMeowWait;
    [SerializeField]
    private AudioClip meow;
    [SerializeField]
    private float meowVolume;
    [SerializeField]
    private AudioClip growl;
    [SerializeField]
    private float growlVolume;

    public float hunger { get; private set; }
    public float MaxHunger { get { return maxHunger; } }

    private float meowTimer = 0f;



    private void Start()
    {
        Application.targetFrameRate = 90;
        hunger = maxHunger;
    }

    private void Update()
    {
        hunger -= hungerLossRate * Time.deltaTime;

        if (hunger <= 0)
            SceneManager.LoadScene(gameOverScene);

        meowTimer -= Time.deltaTime;

        if(meowTimer <= 0)
        {
            meowTimer = Random.Range(minMeowWait, maxMeowWait);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(meow, meowVolume);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == foodTag)
        {
            Destroy(hit.gameObject);
            hunger = maxHunger;
            audioSource.PlayOneShot(growl, growlVolume);
        }
    }

}
