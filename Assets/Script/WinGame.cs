using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject BattleUI;
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject P1CharSelect;
    [SerializeField] private GameObject P2CharSelect;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject RematchButton;
    [SerializeField] private GameObject BackToMenuButton;

    public BattleManager battleManager;
    
    public void rematch() {     
        BattleUI.SetActive(false);
        MenuUI.SetActive(true);
        P1CharSelect.SetActive(true);
        P2CharSelect.SetActive(false);
        Menu.SetActive(false);
        battleManager.Round = 1;
        battleManager.BattleWait = false;
        battleManager.BattleEnd.enabled = false;
        battleManager.CurrentTime = 20f;
        battleManager.timer.text = "20";
        RematchButton.SetActive(false);
        BackToMenuButton.SetActive(false);
        Destroy(battleManager.Player1);
        Destroy(battleManager.Player2);
        /*CharacterManagement.p1Option = 0;
        CharacterManagement.p2Option = 0;*/
    }

    public void backToMenu() {
        BattleUI.SetActive(false);
        MenuUI.SetActive(true);
        P1CharSelect.SetActive(false);
        P2CharSelect.SetActive(false);
        Menu.SetActive(true);
        battleManager.Round = 1;
        battleManager.BattleWait = false;
        battleManager.BattleEnd.enabled = false;
        battleManager.CurrentTime = 20f;
        battleManager.timer.text = "20";
        RematchButton.SetActive(false);
        BackToMenuButton.SetActive(false);
        Destroy(battleManager.Player1);
        Destroy(battleManager.Player2);
    }

}
