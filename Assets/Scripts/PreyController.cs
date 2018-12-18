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
    private float obstacleVoidDist;
    
    public Transform hunter;

    private float turnTimer;
    private int turnDir;
    private bool running = false;
    private bool lastRunStatus = false;
    private int runningTurnDir;

    private float rot;
    private float runningRot = 0f;



    private void Start()
    {
        rot = transform.eulerAngles.x;
        runningTurnDir = Random.Range(0, 2) == 0 ? -1 : 1;
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
            if (runningRot <= -runningRotLimit || runningRot >= runningRotLimit)
                runningTurnDir *= -1;

            transform.eulerAngles += Vector3.up * runningRot;

            //Debug.DrawLine(transform.position, transform.position + dir * obstacleVoidDist);
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, dir, out hit, obstacleVoidDist))
            //{
            //    runningRot += Time.deltaTime * runningTurnDir * runningTurnSpeed;
            //    transform.eulerAngles += Vector3.up * runningRot;
            //}
            //else
            //{
            //    runningRot = 0f;
            //}
        }

        Vector3 move = transform.forward * Time.deltaTime * moveSpeed;
        cc.SimpleMove(move);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == obstacleTag)
            rot += 180f;
    }

}
