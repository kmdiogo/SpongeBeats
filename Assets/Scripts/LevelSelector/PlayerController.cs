using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed;

    Rigidbody rigi;
    Animator anim;
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Basic wasd movement
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey("s"))
        {
            transform.position += -transform.forward * Time.deltaTime * moveSpeed;
            anim.SetBool("isRunning", true);
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play 'nyeh squidward?' when the player touches him
        if (collision.gameObject.CompareTag("Squidward"))
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("NyehSquidward");
        }
        // Randomly play one of patrick's quotes when touched by player
        else if (collision.gameObject.CompareTag("Patrick"))
        {
            AudioManager.instance.StopAll();
            int r = Random.Range(0, 7);
            switch (r)
            {
                case 0:
                    AudioManager.instance.Play("Leedle");
                    break;
                case 1:
                    AudioManager.instance.Play("Pinhead");
                    break;
                case 2:
                    AudioManager.instance.Play("Barnacle");
                    break;
                case 3:
                    AudioManager.instance.Play("Patrick");
                    break;
                case 4:
                    AudioManager.instance.Play("HatSir");
                    break;
                case 5:
                    AudioManager.instance.Play("Mayonnaise");
                    break;
            }
        }

    }

    // Start dance level when krusty krab entrance is hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KrustyKrabEntrance"))
        {
            SceneManager.LoadScene("Spongebeat_Level_1", LoadSceneMode.Single);
        }
    }
}
