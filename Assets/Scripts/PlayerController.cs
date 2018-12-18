using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private CharacterController cc;

    Vector3 rot = Vector3.zero;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 move = transform.forward * Input.GetAxis("Vertical");
        move += transform.right * Input.GetAxis("Horizontal");
        move.Normalize();

        cc.SimpleMove(move * moveSpeed * Time.deltaTime);

        Vector3 deltarot = Vector3.right * -Input.GetAxis("Mouse Y") * lookSensitivity;
        deltarot += Vector3.up * Input.GetAxis("Mouse X") * lookSensitivity;
        rot += deltarot;
        rot.x = Mathf.Clamp(rot.x, -89f, 89f);
        transform.eulerAngles = rot;
    }

}
