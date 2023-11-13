using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManagement : MonoBehaviour
{
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Blue;
    [SerializeField] private GameObject Green;
    [SerializeField] private GameObject Purple;
    
    public CharacterDatabase characterDB;
    public Text nameText;
    public BattleManager battleManager;
    public CharSelect charSelect;
    public CameraObject camera;
    public ScoreUI scoreUIp1;
    public ScoreUI scoreUIp2;
    public HealthBarScript hpP1;
    public FactorBarScript fpP1;
    public HealthBarScript hpP2;
    public FactorBarScript fpP2;
    public static int p1Option;
    public static int p2Option;

    void Start()
    {
        Red.SetActive(true);
        Blue.SetActive(false);
        Green.SetActive(false);
        Purple.SetActive(false);
        UpdateP1Character(p1Option);
        UpdateP2Character(p2Option);
    }

    public void P1NextOption() {
        p1Option++;

        if(p1Option >= characterDB.CharacterCount) {
            p1Option = 0;
        }

        UpdateP1Character(p1Option);
    }

    public void P1PreviousOption() {
        p1Option--;

        if(p1Option < 0) {
            p1Option = characterDB.CharacterCount - 1;
        }

        UpdateP1Character(p1Option);
    }

    public void P2NextOption() {
        p2Option++;

        if(p2Option >= characterDB.CharacterCount) {
            p2Option = 0;
        }

        UpdateP2Character(p2Option);
    }

    public void P2PreviousOption() {
        p2Option--;

        if(p2Option < 0) {
            p2Option = characterDB.CharacterCount - 1;
        }

        UpdateP2Character(p2Option);
    }

    public void NextPlayerOption() {
        battleManager.Player1 = charSelect.Player1;
        battleManager.Player1.GetComponent<PlayerController>().battleManager = this.battleManager;
        camera.player1 = battleManager.Player1.GetComponent<Transform>();
        scoreUIp1.playerData = battleManager.Player1.GetComponent<PlayerData>();
        hpP1.playerData = battleManager.Player1.GetComponent<PlayerData>();
        fpP1.playerData = battleManager.Player1.GetComponent<PlayerData>();
        Debug.Log("Loaded");
    }

    public void PlayOption() {
        battleManager.Player2 = charSelect.Player2;
        battleManager.Player1.GetComponent<PlayerController>().enemy = GameObject.FindWithTag("Player2");
        battleManager.Player2.GetComponent<PlayerController>().enemy = GameObject.FindWithTag("Player1");
        battleManager.Player2.GetComponent<PlayerController>().battleManager = this.battleManager;
        camera.player2 = battleManager.Player2.GetComponent<Transform>();
        scoreUIp2.playerData = battleManager.Player2.GetComponent<PlayerData>();
        hpP2.playerData = battleManager.Player2.GetComponent<PlayerData>();
        fpP2.playerData = battleManager.Player2.GetComponent<PlayerData>();

        battleManager.Player1.GetComponent<CapsuleCollider2D>().enabled = true;
        battleManager.Player2.GetComponent<CapsuleCollider2D>().enabled = true;

        battleManager.BattleStartState = true;
        Debug.Log("Loaded");
    }

    private void UpdateP1Character(int p1Option) {
        Character character = characterDB.GetCharacter(p1Option);
        nameText.text = character.characterName;
        if(p1Option == 0)
        {
            Red.SetActive(true);
            Blue.SetActive(false);
            Green.SetActive(false);
            Purple.SetActive(false);
        }
        else if(p1Option == 1)
        {
            Red.SetActive(false);
            Blue.SetActive(true);
            Green.SetActive(false);
            Purple.SetActive(false);
        }
        else if(p1Option == 2)
        {
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(true);
            Purple.SetActive(false);
        }
        else if(p1Option == 3)
        {
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(false);
            Purple.SetActive(true);
        }
        else
        {
            Debug.Log("Unloaded");
        }
    }

    private void UpdateP2Character(int p2Option) {
        Character character = characterDB.GetCharacter(p2Option);
        nameText.text = character.characterName;
        if(p2Option == 0)
        {
            Red.SetActive(true);
            Blue.SetActive(false);
            Green.SetActive(false);
            Purple.SetActive(false);
        }
        else if(p2Option == 1)
        {
            Red.SetActive(false);
            Blue.SetActive(true);
            Green.SetActive(false);
            Purple.SetActive(false);
        }
        else if(p2Option == 2)
        {
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(true);
            Purple.SetActive(false);
        }
        else if(p2Option == 3)
        {
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(false);
            Purple.SetActive(true);
        }
        else
        {
            Debug.Log("Unloaded");
        }
    }
}
