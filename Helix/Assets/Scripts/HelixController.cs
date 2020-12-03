using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    
    [SerializeField] private GameObject _propeller;
    [SerializeField] private GameObject _rotor;
    [SerializeField] private GameObject _aim;
    
    // Start is called before the first frame update
    void Start()
    {
        if(this._propeller == null || this._rotor == null || this._aim == null)
        {
            Debug.LogError("Não existe atribuiçao para _propeller | _rotor | _aim");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotação das helices
        this._propeller.transform.Rotate(0.0f, Constant.PROPELLER_SPEED * Time.deltaTime, 0.0f);
        this._rotor.transform.Rotate(Constant.ROTOR_SPEED * Time.deltaTime, 0.0f, 0.0f);
    }

    public void setAngle(float angleX, float angleY, float angleZ)
    {
        this.transform.localRotation = Quaternion.Euler(angleX, angleY, angleZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constant.TAG_BOMB)
        {
            LevelManager levelManager = GameObject.FindGameObjectWithTag(Constant.TAG_LEVEL_MANAGER).GetComponent<LevelManager>();
            levelManager.damageOnPlayer();
            levelManager.addPlayerHitCounter();
            levelManager.updateGameUserInterface();
            levelManager.tryUpdateGameState();
            Destroy(collision.gameObject);
        }
    }

    public bool shoot()
    {
        Debug.DrawRay(this._aim.transform.position, this._aim.transform.forward * Constant.SHOOT_RANGE, Color.white, 0.5f);
        RaycastHit raycastHit;
        if (Physics.Raycast(this._aim.transform.position, this._aim.transform.forward, out raycastHit, Constant.SHOOT_RANGE))
        {
            Destroy(raycastHit.collider.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
}
