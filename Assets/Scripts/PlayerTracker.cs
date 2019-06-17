using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        relativePosition = this.transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null)
            return;
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + relativePosition + playerTransform.forward * 1.5f, 0.05f);
    }
}
