using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // If note is in one of the arrow buttons
            if (canBePressed)
            {
                gameObject.SetActive(false);

                // Check how close to center note was when key was hit to determine how if hit, good, or perfect
                var yPos = Mathf.Abs(transform.position.y);
                if (yPos > 0.25f)
                {
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (yPos > 0.09f)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, hitEffect.transform.rotation);
                }
                else
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Handle missed note
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, hitEffect.transform.rotation);
        }
    }
}
