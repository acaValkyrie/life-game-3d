using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    Camera _mainCamera;
    GameObject _cameraObject;
    private Vector3 _initialPosition;
    Vector3 lookAtPosition = new Vector3(15, 15, 15);
    private float _theta = 0f;
    private float _phi = 0f;
    private float _radius;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _cameraObject = _mainCamera.gameObject;
        _initialPosition = _cameraObject.transform.position;
        _radius = Vector3.Distance(_initialPosition, lookAtPosition);
        _cameraObject.transform.LookAt(lookAtPosition);
        _theta = math.acos(_initialPosition.x/_radius);
        _phi = math.asin(_initialPosition.y/_radius);
    }
    
    void SetPosition(float theta, float phi)
    {
        float x = lookAtPosition.x + _radius * Mathf.Cos(theta);
        float y = lookAtPosition.y + _radius * Mathf.Sin(phi);
        float z = lookAtPosition.z + _radius * Mathf.Sin(theta);
        _cameraObject.transform.position = new Vector3(x, y, z);
        _cameraObject.transform.LookAt(lookAtPosition);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTheta = 0f;
        float deltaPhi = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            deltaTheta = 0.1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            deltaTheta = -0.1f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            deltaPhi = 0.1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            deltaPhi = -0.1f;
        }
        _theta += deltaTheta;
        _phi += deltaPhi;
        SetPosition(_theta, _phi);
    }
}
