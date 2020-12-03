using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public Text hpText;
    public Text hitText;
    public Text gameStateMsg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHP(string msg)
    {
        this.hpText.text = msg;
    }
    public void setHit(string msg)
    {
        this.hitText.text = msg;
    }
    public void setGameStateMsg(string msg)
    {
        this.gameStateMsg.text = msg;
    }
}
