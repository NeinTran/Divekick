using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Nodes;
    public Sprite EmptyNode;
    public Sprite WinNode;
    public PlayerData playerData;
    void Start() {

    }
    void Update() {
        for (int i = 0; i < playerData.Score; i++)
        {
            Nodes[i].gameObject.GetComponent<Image>().sprite = WinNode;
        }
    }

    void Reset()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i].gameObject.GetComponent<Image>().sprite = EmptyNode;
        }
    }
}
