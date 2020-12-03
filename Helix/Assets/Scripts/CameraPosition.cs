using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private CameraMode _cameraMode;

    public CameraMode getCameraMode()
    {
        return this._cameraMode;
    }
}
