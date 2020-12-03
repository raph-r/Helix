using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _lookAtPoint;
    private Dictionary<CameraMode, GameObject> _cameraPosition;

    private bool isCameraPositionChanging = false;

    void Awake()
    {
        this._cameraPosition = new Dictionary<CameraMode, GameObject>();
        this.loadCameraPosition();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void loadCameraPosition()
    {
        GameObject[] cameraPositions = GameObject.FindGameObjectsWithTag(Constant.TAG_CAMERA_POSITION);
        foreach (GameObject cameraPosition in cameraPositions)
        {
            this._cameraPosition.Add(cameraPosition.GetComponent<CameraPosition>().getCameraMode(), cameraPosition);
        }
    }

    public void changeCameraPosition(CameraMode cameraMode, GameObject lookAt)
    {
        if(!this.isCameraPositionChanging)
        {
            StartCoroutine(
                this.smoothLookAt(this._cameraPosition[cameraMode], lookAt)
            );
        }
    }

    private IEnumerator smoothLookAt(GameObject gameObjFinalPosition, GameObject gameObjLookAt)
    {
        this.isCameraPositionChanging = true;
        float elapseTime = 0.0f;
        Vector3 startPosition = this.transform.localPosition;
        while(elapseTime < Constant.DURATION_OF_CHANGING_CAMERA_POSITION)
        {
            transform.localRotation = Quaternion.RotateTowards(
                this.transform.localRotation,
                Quaternion.LookRotation(
                    gameObjLookAt.transform.localPosition - this.transform.localPosition
                ),
                (elapseTime / Constant.DURATION_OF_CHANGING_CAMERA_POSITION)
            );
            this.transform.localPosition = Vector3.Lerp(startPosition, gameObjFinalPosition.transform.localPosition, (elapseTime / Constant.DURATION_OF_CHANGING_CAMERA_POSITION));
            elapseTime += Time.deltaTime;
            yield return null;  
        }   
        this.transform.localPosition = gameObjFinalPosition.transform.localPosition;
        this.transform.LookAt(gameObjLookAt.transform.position);
        this.isCameraPositionChanging = false;
    }
}
