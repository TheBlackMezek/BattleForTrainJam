using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyController : MonoBehaviour {

    [SerializeField]
    private CharacterController cc;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float runningTurnSpeed;
    [SerializeField]
    private float minTurnLength;
    [SerializeField]
    private float maxTurnLength;
    [SerializeField]
    private float minRunningTurnLength;
    [SerializeField]
    private float maxRunningTurnLength;
    [SerializeField]
    private float runningRotLimit;
    [SerializeField]
    private string obstacleTag;
    [SerializeField]
    private float lookDist;
    [SerializeField]
    private float obstacleAvoidDist;
    
    public Transform hunter;

    [Header("Audio")]

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float minPitch;
    [SerializeField]
    private float maxPitch;
    [SerializeField]
    private float minSqueakWait;
    [SerializeField]
    private float maxSqueakWait;

    private float turnTimer;
    private int turnDir;
    private bool running = false;
    private bool lastRunStatus = false;
    private int runningTurnDir;

    private float rot;
    private float runningRot = 0f;

    private float squeakTimer;



    private void Start()
    {
        rot = transform.eulerAngles.x;
        runningTurnDir = Random.Range(0, 2) == 0 ? -1 : 1;
        squeakTimer = Random.Range(minSqueakWait, maxSqueakWait);
    }

    private void Update()
    {
        running = Vector3.Distance(transform.position, hunter.position) < lookDist ? true : false;
        if(running != lastRunStatus)
        {
            lastRunStatus = running;
            runningRot = 0f;
            turnTimer = 0f;
        }

        if(!running)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                turnDir = Random.Range(0, 2) == 0 ? -1 : 1;
                turnTimer = Random.Range(minTurnLength, maxTurnLength);
            }

            rot += Time.deltaTime * turnDir * turnSpeed;
            transform.eulerAngles = Vector3.up * rot;
        }
        else
        {
            Vector3 dir = (transform.position - hunter.position).normalized;
            Vector3 point = transform.position + dir * lookDist;

            transform.LookAt(point);

            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                runningTurnDir = Random.Range(0, 2) == 0 ? -1 : 1;
                turnTimer = Random.Range(minRunningTurnLength, maxRunningTurnLength);
            }
            
            runningRot += Time.deltaTime * runningTurnDir * runningTurnSpeed;
            if (runningRot <= -runningRotLimit)
            {
                runningTurnDir = 1;
            }
            else if(runningRot >= runningRotLimit)
            {
                runningTurnDir = -1;
            }

            //Debug.DrawLine(transform.position, transform.position + transform.forward * obstacleAvoidDist);
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, dir, out hit, obstacleAvoidDist))
            //{
            //    //if(hit.transform.tag == "Player")
            //    //{
            //    //    Debug.Log("PLAYER");
            //    //    runningRot = 0f;
            //    //    transform.LookAt(point);
            //    //}
            //    //else
            //    //{
            //        runningRot += 90f * runningTurnDir * (Vector3.Distance(hit.point, transform.position) / obstacleAvoidDist);
            //    //}
            //}

            transform.eulerAngles += Vector3.up * runningRot;
        }

        Vector3 move = transform.forward * Time.deltaTime * moveSpeed;
        cc.SimpleMove(move);

        squeakTimer -= Time.deltaTime;
        if(squeakTimer <= 0)
        {
            squeakTimer = Random.Range(minSqueakWait, maxSqueakWait);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == obstacleTag)
            rot += 180f;
    }

}
