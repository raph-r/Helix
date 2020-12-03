using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject _helix;
    [SerializeField] private GameObject _lookAt;
    [SerializeField] private GameObject _gameUserInterface;
    private Camera _camera;
    private GameState _gameState;
    private int _playerHitCounter;
    private int _playerHP;
    private int _bombCounter;

    // Start is called before the first frame update
    void Start()
    {
        this._gameState = GameState.PLAY;
        //  Inicializa o contador de hits do jogador
        this._playerHitCounter = 0;
        //  Define o HP do Jogador
        this._playerHP = Constant.PLAYER_MAX_HP;

        // atualiza o GUI
        this.updateGameUserInterface();

        //  adiciona a referencia para LevelManager
        this._playerController.GetComponent<PlayerController>().levelManager = this;

        // captura a referencia para camera
        this._camera = this._playerController.GetComponentInChildren<Camera>();

        // Camera padrão
        this.setCameraMode(CameraMode.FOLLOW_BACK);

        // Efetua a contagem de bombas a serem destruidas
        this._bombCounter = GameObject.FindGameObjectsWithTag(Constant.TAG_BOMB).Length;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this._gameState)
        {
            case GameState.PLAY:
                break;
            case GameState.GAMEOVER:
                this.setGameStateMsg(Constant.MSG_GAME_OVER);
                Destroy(this._helix);
                this._gameState = GameState.RESULT;
                InputSystem.DisableAllEnabledActions();
                break;
            case GameState.WIN:
                this.setGameStateMsg(Constant.MSG_WIN);
                this._gameState = GameState.RESULT;
                InputSystem.DisableAllEnabledActions();
                break;
            case GameState.RESULT:
                break;
            default:
                break;
        }
    }

    public void setCameraMode(CameraMode cameraMode)
    {
        this._camera.changeCameraPosition(
            cameraMode,
            this._lookAt
        );
    }

    public void addPlayerHitCounter()
    {
        this._playerHitCounter++;
    }

    public void damageOnPlayer(int damage = Constant.LOWER_DAMAGE_ON_PLAYER)
    {
        this._playerHP -= damage;
        if (this._playerHP < 0)
        {
            this._playerHP = 0;
        }
    }

    public void tryUpdateGameState()
    {
        if (this._playerHP <= 0)
        {
            this._gameState = GameState.GAMEOVER;
        }
        else if (this._playerHitCounter == this._bombCounter)
        {
            this._gameState = GameState.WIN;
        }
    }

    public void updateGameUserInterface()
    {
        GUIController gUIController = this._gameUserInterface.GetComponent<GUIController>();
        gUIController.setHit(this._playerHitCounter.ToString());
        gUIController.setHP(this._playerHP.ToString());
    }

    private void setGameStateMsg(string msg)
    {
        this._gameUserInterface.GetComponent<GUIController>().setGameStateMsg(msg);
    }
}
