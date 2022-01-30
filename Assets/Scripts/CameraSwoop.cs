using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSwoop : MonoBehaviour
{
    public Vector3 startPosition;
    public Quaternion startRotation;

    private Vector3 endPosition;
    private Quaternion endRotation;

    // Start is called before the first frame update
    void Start()
    {
        endPosition = transform.position;
        endRotation = transform.rotation;
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.DORotateQuaternion(endRotation, 4f);
        transform.DOMove(endPosition, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
