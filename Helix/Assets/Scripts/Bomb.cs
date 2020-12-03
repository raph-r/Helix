using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public const float _rotationSpeed = 20.0f;
    private float _currentRotationSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this._currentRotationSpeed = Bomb._rotationSpeed * Time.deltaTime;
        this.transform.Rotate(this._currentRotationSpeed, this._currentRotationSpeed, this._currentRotationSpeed);
    }
}