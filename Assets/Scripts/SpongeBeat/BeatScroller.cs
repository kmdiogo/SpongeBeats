using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float tempo;
    public bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {
        tempo /= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            
        }
        else
        {
            // Move notes down relative to the songs Beats per second and time
            transform.position -= Vector3.up * (tempo * Time.deltaTime);
        }
    }
}
