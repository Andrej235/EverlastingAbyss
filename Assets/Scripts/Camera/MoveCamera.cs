using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPosition.position, Time.deltaTime * 10f);
    }
}
