using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongebobObject : MonoBehaviour
{
    public float rotationSpeed = 20f;
    private Vector3 initial;
    // Start is called before the first frame update
    void Start()
    {
        initial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initial;

        // Set rotation to face camera
        if (GameManager.instance.gameOverHasTriggered)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            this.enabled = false;
            return;
        }

        // Slowly rotate spongebob while game is playing
        if (GameManager.instance.startPlaying && !GameManager.instance.isGameOver)
        {
            transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
        }



    }
}
