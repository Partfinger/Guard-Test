using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float RotateSpeed;

    private Rigidbody rb;

    public CameraRotateAround cameraScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Movement()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                                                  transform.rotation.eulerAngles.y + Input.GetAxis("Horizontal"),
                                                                                  transform.rotation.eulerAngles.z), Time.fixedDeltaTime * RotateSpeed);
        rb.AddForce(transform.forward * Speed * Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void Stop()
    {
        enabled = false;
        cameraScript.Stop();
    }

}
