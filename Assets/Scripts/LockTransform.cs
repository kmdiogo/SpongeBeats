using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initPos;
    private Quaternion initRot;
    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }
}
