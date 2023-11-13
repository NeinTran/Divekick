using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Text RoundStart;
    [SerializeField] private Text RoundEnd;
    [SerializeField] private Text NextRound;
    [SerializeField] private Text BattleStart;
    [SerializeField] private Text Timeout;
    //[SerializeField] private GameObject Rematch;
    //[SerializeField] private GameObject BackToMenu;
    [SerializeField] private Transform MidPoint;
    //[SerializeField] private GameObject RematchButton;
    //[SerializeField] private GameObject BackToMenuButton;
    [SerializeField] private float Player1InitialX;
    [SerializeField] private float Player1InitialY;
    [SerializeField] private float Player2InitialX;
    [SerializeField] private float Player2InitialY;


    public GameObject Player1;
    public GameObject Player2;
    public bool BattleWait;
    public bool BattleStartState;
    public Text BattleEnd;
    public Text timer;
    public int Round = 1;
    private float AnnouncementDuration = 2f;
    private int WinScore = 5;
    public float CurrentTime;
    private float StartTime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        //CurrentTime = StartTime;
        //Rematch.SetActive(false);
        //BackToMenu.SetActive(false);
        Timeout.enabled = false;
        RoundStart.enabled = false;
        RoundEnd.enabled = false;
        NextRound.enabled = false;
        BattleEnd.enabled = false;
        BattleStartState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleStartState == true)
        {
            StartCoroutine(BattleStartAnnouncement());
        }
        NextRound.text = "Round " + Round;
        if (BattleWait == true)
        {
            Player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        RunTimer();
    }

    public IEnumerator RoundStartAnnouncement()
    {
        RoundStart.enabled = true;
        yield return new WaitForSeconds(0.75f);
        RoundStart.enabled = false;
    }
    public IEnumerator NextRoundAnnouncement()
    {
        NextRound.enabled = true;
        ResetTimer();
        
        Player1.transform.position = new Vector3(Player1InitialX, Player1InitialY, 0);
        Player2.transform.position = new Vector3(Player2InitialX, Player2InitialY, 0);

        Player1.GetComponent<PlayerController>().animator.SetTrigger("Idling");
        Player1.GetComponent<PlayerController>().State = "idle";
        Player1.GetComponent<PlayerData>().Health = 1;
        if (Player1.GetComponent<PlayerController>().Factor == true)
        {
            Player1.GetComponent<PlayerData>().CurrentKickMeter = 0;
        }
        Player1.GetComponent<CapsuleCollider2D>().enabled = true;
        Player1.GetComponent<Rigidbody2D>().gravityScale = 4;

        Player2.GetComponent<PlayerController>().animator.SetTrigger("Idling");
        Player2.GetComponent<PlayerController>().State = "idle";
        Player2.GetComponent<PlayerData>().Health = 1;
        if (Player2.GetComponent<PlayerController>().Factor == true)
        {
            Player2.GetComponent<PlayerData>().CurrentKickMeter = 0;          
        }
        Player2.GetComponent<CapsuleCollider2D>().enabled = true;
        Player2.GetComponent<Rigidbody2D>().gravityScale = 4;
        yield return new WaitForSeconds(AnnouncementDuration);
        Player1.GetComponent<PlayerController>().animator.ResetTrigger("Idling");
        Player2.GetComponent<PlayerController>().animator.ResetTrigger("Idling");
        StartCoroutine(RoundStartAnnouncement());
        BattleWait = false;
        NextRound.enabled = false;
    }

    public IEnumerator FirstRoundAnnouncement()
    {
        NextRound.enabled = true;
        ResetTimer();
        
        Player1.transform.position = new Vector3(Player1InitialX, Player1InitialY, 0);
        Player2.transform.position = new Vector3(Player2InitialX, Player2InitialY, 0);

        Player1.GetComponent<PlayerController>().State = "idle";
        Player1.GetComponent<PlayerData>().Health = 1;
        if (Player1.GetComponent<PlayerController>().Factor == true)
        {
            Player1.GetComponent<PlayerData>().CurrentKickMeter = 0;
        }
        Player1.GetComponent<CapsuleCollider2D>().enabled = true;
        Player1.GetComponent<Rigidbody2D>().gravityScale = 4;

        Player2.GetComponent<PlayerController>().State = "idle";
        Player2.GetComponent<PlayerData>().Health = 1;
        if (Player2.GetComponent<PlayerController>().Factor == true)
        {
            Player2.GetComponent<PlayerData>().CurrentKickMeter = 0;          
        }
        Player2.GetComponent<CapsuleCollider2D>().enabled = true;
        Player2.GetComponent<Rigidbody2D>().gravityScale = 4;
        yield return new WaitForSeconds(AnnouncementDuration);
        StartCoroutine(RoundStartAnnouncement());
        BattleWait = false;
        NextRound.enabled = false;
    }

    public IEnumerator RoundEndAnnouncement()
    {
        RoundEnd.enabled = true;
        BattleWait = true;
        timer.text = "20";
        yield return new WaitForSeconds(AnnouncementDuration+0.75f);
        RoundEnd.enabled = false;
        Round++;
        if (Player1.GetComponent<PlayerData>().Score == WinScore || Player2.GetComponent<PlayerData>().Score == WinScore)
        {
            StartCoroutine(BattleEndAnnouncement());
            //StartCoroutine(Buttons());
        }
        else
        {
            StartCoroutine(NextRoundAnnouncement());
        }
    }

    public IEnumerator TimeoutAnnouncement()
    {
        Timeout.enabled = true;
        BattleWait = true;
        yield return new WaitForSeconds(AnnouncementDuration+0.75f);
        Timeout.enabled = false;
        Round++;
        if (Player1.GetComponent<PlayerData>().Score == WinScore || Player2.GetComponent<PlayerData>().Score == WinScore)
        {
            StartCoroutine(BattleEndAnnouncement());
            //StartCoroutine(Buttons());
        }
        else
        {
            StartCoroutine(NextRoundAnnouncement());
        }
    }

    public IEnumerator BattleEndAnnouncement()
    {
        if (Player1.GetComponent<PlayerData>().Score == WinScore)
        {
            BattleEnd.text = "Player 1 Wins";
        }
        if (Player2.GetComponent<PlayerData>().Score == WinScore)
        {
            BattleEnd.text = "Player 2 Wins";
        }
        BattleEnd.enabled = true;
        Player1.GetComponent<CapsuleCollider2D>().enabled = false;
        Player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Player1.GetComponent<PlayerData>().Score = 0;

        Player2.GetComponent<CapsuleCollider2D>().enabled = false;
        Player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Player2.GetComponent<PlayerData>().Score = 0;

        Debug.Log("Game ended");
        //RematchButton.SetActive(true);
        //BackToMenuButton.SetActive(true);
        yield return new WaitForSeconds(AnnouncementDuration);
        //BattleEnd.enabled = false;
    }

    public IEnumerator BattleStartAnnouncement()
    {
        BattleStartState = false;
        BattleStart.enabled = true;
        BattleWait = true;
        yield return new WaitForSeconds(AnnouncementDuration);
        BattleStart.enabled = false;
        StartCoroutine(FirstRoundAnnouncement());
    }

    /*public IEnumerator Buttons() 
    {
        Rematch.SetActive(true);
        BackToMenu.SetActive(true);
        yield return new WaitForSeconds(AnnouncementDuration);
    }*/

    public void CheckWinTimeout()
    {
        float P1distance = Mathf.Abs(MidPoint.position.x - Player1.transform.position.x);
        float P2distance = Mathf.Abs(MidPoint.position.x - Player2.transform.position.x);
        if (P1distance < P2distance)
        {
            Player1.GetComponent<PlayerData>().Score++;
            BattleWait = true;
            StartCoroutine(TimeoutAnnouncement());
        }
        else if (P2distance < P1distance)
        {
            Player2.GetComponent<PlayerData>().Score++;
            BattleWait = true;
            StartCoroutine(TimeoutAnnouncement());
        }
    }

    public void ResetTimer()
    {
        CurrentTime = StartTime;
    }

    public void RunTimer()
    {
        if (BattleWait == false)
        {
            if (CurrentTime <= 0)
            {
                CheckWinTimeout();
            }
            CurrentTime -= 1 * Time.deltaTime;
            timer.text = Mathf.Round(CurrentTime).ToString();
        }
    }
}
