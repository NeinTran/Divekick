using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    [SerializeField] private GameObject Grid;
    [SerializeField] private GameObject BattleUI;
    [SerializeField] private GameObject P1Green;
    [SerializeField] private GameObject P2Green;
    [SerializeField] private GameObject P1Purple;
    [SerializeField] private GameObject P2Purple;
    [SerializeField] private GameObject P1Blue;
    [SerializeField] private GameObject P2Blue;
    [SerializeField] private GameObject P1Red;
    [SerializeField] private GameObject P2Red;
    
    public GameObject Player1;
    public GameObject Player2;
    public CharacterDatabase characterDB;
    public GameObject MenuUI;
    

    public void Next() 
    {
        if (CharacterManagement.p1Option == 0) {
            Player1 = Instantiate(P1Red, new Vector3(-17, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player2") == null)
            {
                Debug.Log("found1");
            }
        }
        else if (CharacterManagement.p1Option == 1) {
            Player1 = Instantiate(P1Blue, new Vector3(-17, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player2") == null)
            {
                Debug.Log("found1");
            }
        }
        else if (CharacterManagement.p1Option == 2) {
            Player1 = Instantiate(P1Green, new Vector3(-17, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player2") == null)
            {
                Debug.Log("found1");
            }
        }
        else if (CharacterManagement.p1Option == 3) {
            Player1 = Instantiate(P1Purple, new Vector3(-17, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player2") == null)
            {
                Debug.Log("found1");
            }
        }
        else {
            Debug.Log("Failed to load p1 char");
        }
    }
    
    public void Play() {      
        MenuUI.SetActive(false);
        BattleUI.SetActive(true);
        if (CharacterManagement.p2Option == 0) {
            Player2 = Instantiate(P2Red, new Vector3(22, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player1") != null)
            {
                Debug.Log("found");
            }
        }
        else if (CharacterManagement.p2Option == 1) {
            Player2 = Instantiate(P2Blue, new Vector3(22, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player1") != null)
            {
                Debug.Log("found");
            }
        }
        else if (CharacterManagement.p2Option == 2) {
            Player2 = Instantiate(P2Green, new Vector3(22, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player1") != null)
            {
                Debug.Log("found");
            }
        }
        else if (CharacterManagement.p2Option == 3) {
            Player2 = Instantiate(P2Purple, new Vector3(22, -7.456481f, 0), Quaternion.identity);
            if (GameObject.FindWithTag("Player1") != null)
            {
                Debug.Log("found");
            }
        }
        else {
            Debug.Log("Failed to load p2 char");
        }
    }

    public void Back() {
        Destroy(GameObject.FindWithTag("Player1"));
        Destroy(GameObject.FindWithTag("Player2"));
    }

}
