using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 20f;
    private bool playScene = false;

    private void Start()
    {
        StartCoroutine(goScene()); 
    }

    void Update()
    {
        if (playScene == true) {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.Normalize();
            transform.position = transform.position + movement * speed * Time.deltaTime;

            if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator goScene()
    {
        yield return new WaitForSeconds(3.5f);
        playScene = true;
    }

}
