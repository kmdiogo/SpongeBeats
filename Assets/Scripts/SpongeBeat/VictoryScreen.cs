using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;
    public Camera mainCamera;

    bool hasMoved;
    void Start()
    {
        hasMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver && !hasMoved)
        {
            Invoke("MoveCameraToVictoryScreen", 5);
        }
    }

    void MoveCameraToVictoryScreen()
    {
        mainCamera.transform.position = victoryScreen.transform.position;
    }
}
