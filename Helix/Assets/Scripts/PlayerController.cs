using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector3 _movementInputState;
    private Vector3 _speedMultipliers;
    private float _rotationStateY;
    public LevelManager levelManager { get; set; }

    private HelixController _helixController;

    // Start is called before the first frame update
    void Start()
    {
        this._helixController = this.transform.Find("Helix").GetComponent<HelixController>();
        this._movementInputState = new Vector3();
        this._speedMultipliers = new Vector3(Constant.MOVE_SPEED, Constant.UP_SPEED, Constant.MOVE_SPEED);
        this._rotationStateY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._movementInputState.y < 0 && this.transform.position.y < Constant.MIN_HEIGHT)
        {
            this._movementInputState.y = 0.0f;
        }
        // Movimento
        this.transform.Translate(Vector3.Scale(this._movementInputState, this._speedMultipliers) * Time.deltaTime);

        // Rotacao atraves de input do jogador
        this.transform.Rotate(0.0f, (this._rotationStateY * Constant.ROTATION_SPEED * Time.deltaTime), 0.0f);

        // Inclina a fuselagem 
        this._helixController.setAngle(this._movementInputState.z * Constant.TILT_ANGLE, 0.0f, -this._movementInputState.x * Constant.TILT_ANGLE);
    }

    void FixedUpdate()
    {

    }

    public void setMovementInputStateXZ(InputAction.CallbackContext context)
    {
        Vector2 newInputStateXY = context.ReadValue<Vector2>();
        this._movementInputState.x = newInputStateXY.x;
        this._movementInputState.z = newInputStateXY.y;
    }

    public void setMovementInputStateY(InputAction.CallbackContext context)
    {
        this._movementInputState.y = context.ReadValue<float>();
    }

    public void setRotationStateY(InputAction.CallbackContext context)
    {
        this._rotationStateY = context.ReadValue<float>();
    }

    public void setCameraMode(InputAction.CallbackContext context)
    {
        this.levelManager.setCameraMode((CameraMode)int.Parse(context.control.name));
    }

    public void shoot()
    {
        if (this._helixController.shoot())
        {
            this.levelManager.addPlayerHitCounter();
            this.levelManager.tryUpdateGameState();
            this.levelManager.updateGameUserInterface();
        }
    }
}
