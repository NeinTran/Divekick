using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region SerializeVariables
    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform FrontWallCheck;
    [SerializeField] private Transform BackWallCheck;
    [SerializeField] private float DiveLimit;
    [SerializeField] private float KickLimit;
    [SerializeField] private Transform DivekickAttackPoint;
    [SerializeField] private Transform GroundkickAttackPoint;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private LayerMask WhatIsEnemy;
    [SerializeField] private Collider2D Hitbox;
    [SerializeField] private PlayerData playerdata;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject FactorFire;
    [SerializeField] private bool canBeGuarded;
    [SerializeField] private bool canBeFactor;
    #endregion

    #region PrivateVariables
    private bool Grounded;
    private bool isBlock = false;
    private bool Special = false;
    private bool isDashing = false;
    private float OriginalGravity;
    private float OriginalDivekickForward;
    private float OriginalDivekickDown;
    private float OriginalDiveUp;
    private float OriginalKickBack;
    private Vector3 OriginalScale;
    private float kickTime;
    private float diveTime;
    private float kickTime2;
    private float diveTime2;
    private bool kickPressed = false;
    private bool divePressed = false;
    private bool kickPressed2 = false;
    private bool divePressed2 = false;
    #endregion 

    #region Constant
    const float GroundRadius = 0.08f;
    const float AttackRadius = 0.1f;
    #endregion

    #region PublicVariables
    public BattleManager battleManager;
    public GameObject enemy;
    public Animator animator;
    public UnityEvent OnLandEvent;
    public UnityEvent OnWallEvent;
    public string State;
    public bool Factor = false;
    #endregion
    // Start is called before the first frame update
    public void Awake() 
    {
        FactorFire.SetActive(false);
        Shield.SetActive(false);
        OriginalGravity = rgbd.gravityScale;
        OriginalScale = gameObject.transform.localScale;
        if (OnLandEvent == null) {
            OnLandEvent = new UnityEvent();
        }
        OriginalDivekickForward = playerdata.DivekickForceForward;
        OriginalDivekickDown = playerdata.DivekickForceDown;
        OriginalDiveUp = playerdata.DiveForce;
        OriginalKickBack = playerdata.KickForceBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) 
        {
            return;
        }
        if (gameObject.CompareTag("Player1")) 
        {
            if (State == "startdive" && (gameObject.transform.position.y >= DiveLimit))
            {
                State = "dive";
            }
            if (State == "startkick" && (gameObject.transform.position.y >= KickLimit))
            {
                State = "kick";
            }
            //StateAnimation();
            if (Input.GetButtonDown("Kick") && State != "wait") 
            {
                kickPressed = true;
                kickTime = Time.time;
                if (divePressed && (Time.time - diveTime <= 0.1f) && State != "death")
                {
                    Special = true;
                    kickPressed = false;
                    divePressed = false;
                }
                else if ((State == "dive" || State == "kick" || State == "walldive") && State != "death" && (Time.time - diveTime > 0.1f)) 
                {
                    State = "divekick";
                    if ((playerdata.CurrentKickMeter < playerdata.MaxKickMeter) && Factor == false) 
                    {
                        playerdata.CurrentKickMeter += playerdata.DivekickMeterCharge;
                    }
                    if (playerdata.CurrentKickMeter > playerdata.MaxKickMeter) 
                    {
                        playerdata.CurrentKickMeter = playerdata.MaxKickMeter;
                    }
                }
                else if (State == "idle" && Grounded && State != "death") 
                {
                    State = "startkick";
                    if ((playerdata.CurrentKickMeter < playerdata.MaxKickMeter) && Factor == false) 
                    {
                        playerdata.CurrentKickMeter += playerdata.KickMeterCharge;
                    }
                    if (playerdata.CurrentKickMeter > playerdata.MaxKickMeter) 
                    {
                        playerdata.CurrentKickMeter = playerdata.MaxKickMeter;
                    }
                }
            }
            if (Input.GetButtonDown("Dive") && State != "wait") 
            {
                divePressed = true;
                diveTime = Time.time;
                if (kickPressed && (Time.time - kickTime <= 0.1f) && State != "death")
                {
                    Special = true;
                    kickPressed = false;
                    divePressed = false;
                }
                else if (State == "idle" && Grounded && State != "death")
                {
                    State = "startdive";
                }
            }
            if (State == "dive" && Special == true) 
            {
                if (playerdata.CurrentKickMeter >= playerdata.SpecialConsume) 
                {
                    State = "airspecial";
                    playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                    Special = false;
                }
            }
            if (Grounded && Special == true && canBeGuarded == false) 
            {
                if (playerdata.CurrentKickMeter >= playerdata.SpecialConsume) 
                {
                    State = "groundspecial";
                    playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                    Special = false;
                }
            }
            else if (Grounded && Special == true && canBeGuarded == true)
            {
                if (playerdata.CurrentKickMeter >= playerdata.SpecialConsume) 
                {
                    playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                    State = "groundblock";
                    Special = false;
                }
            }
            if (playerdata.CurrentKickMeter == playerdata.MaxKickMeter && canBeFactor == true) 
            {
                StartCoroutine(FactorMode());
                if (playerdata.CurrentKickMeter < 0) 
                {
                    playerdata.CurrentKickMeter = 0;
                }
            }
        }

        if (gameObject.CompareTag("Player2")) 
        {
            if (State == "startdive" && (gameObject.transform.position.y >= DiveLimit))
            {
                State = "dive";
            }
            if (State == "startkick" && (gameObject.transform.position.y >= KickLimit))
            {
                State = "kick";
            }
            if (State == "startdive" && (gameObject.transform.position.y >= DiveLimit))
            {
                State = "dive";
            }
            if (Input.GetButtonDown("Kick2") && State != "wait") 
            {
                kickPressed2 = true;
                kickTime = Time.time;
                if (divePressed2 && (Time.time - diveTime <= 0.1f) && State != "death")
                {
                    Special = true;
                    kickPressed = false;
                    divePressed = false;
                }
                else if ((State == "dive" || State == "kick" || State == "walldive") && State != "death" && (Time.time - diveTime > 0.1f)) 
                {
                    State = "divekick";
                    if ((playerdata.CurrentKickMeter < playerdata.MaxKickMeter) && Factor == false) 
                    {
                        playerdata.CurrentKickMeter += playerdata.DivekickMeterCharge;
                    }
                    if (playerdata.CurrentKickMeter > playerdata.MaxKickMeter) 
                    {
                        playerdata.CurrentKickMeter = playerdata.MaxKickMeter;
                    }
                }
                else if (State == "idle" && Grounded && State != "death") 
                {
                    State = "startkick";
                    if ((playerdata.CurrentKickMeter < playerdata.MaxKickMeter) && Factor == false) 
                    {
                        playerdata.CurrentKickMeter += playerdata.KickMeterCharge;
                    }
                    if (playerdata.CurrentKickMeter > playerdata.MaxKickMeter) 
                    {
                        playerdata.CurrentKickMeter = playerdata.MaxKickMeter;
                    }
                }
            }
            if (Input.GetButtonDown("Dive2") && State != "wait") 
            {
                divePressed2 = true;
                diveTime = Time.time;
                if (kickPressed2 && (Time.time - kickTime <= 0.1f) && State != "death")
                {
                    Special = true;
                    kickPressed2 = false;
                    divePressed2 = false;
                }
                else if (State == "idle" && Grounded && State != "death")
                {
                    State = "startdive";
                }
            }
            if (State == "dive" && Special == true) 
            {
                if (playerdata.CurrentKickMeter >= playerdata.SpecialConsume) 
                {
                    State = "airspecial";
                    playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                    Special = false;
                }
            }
            if (Grounded && Special == true && canBeGuarded == false) {
                if (playerdata.CurrentKickMeter >= playerdata.SpecialConsume) 
                {
                    State = "groundspecial";
                    playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                    Special = false;
                }
            }
            else if (Grounded && Special == true && canBeGuarded == true)
            {
                playerdata.CurrentKickMeter -= playerdata.SpecialConsume;
                State = "groundblock";
                Special = false;
            }
            if (playerdata.CurrentKickMeter == playerdata.MaxKickMeter && canBeFactor == true) 
            {
                StartCoroutine(FactorMode());
                if (playerdata.CurrentKickMeter < 0) 
                {
                    playerdata.CurrentKickMeter = 0;
                }
            }
        }
    }

    private void Flip() 
    {
        if (transform.position.x > enemy.transform.position.x) 
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -enemy.transform.localScale.x;
            transform.localScale = theScale;
        } 
        else 
        {
            transform.localScale = OriginalScale;
        }
    }

    public void OnLanding() 
    {
        State = "idle";
        Special = false;
        rgbd.gravityScale = OriginalGravity;
        Debug.Log("landed");
    }

    public void OnWallTouching() 
    {
        State = "walldive";
        Special = false;
        rgbd.gravityScale = OriginalGravity;
        Debug.Log("wall");
    }

    public void FixedUpdate() 
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        if (isDashing) 
        {
            return;
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) 
        {
            if (colliders[i].gameObject != gameObject) 
            {
                Grounded = true;
                if (!wasGrounded) 
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        Collider2D[] frontwalls = Physics2D.OverlapCircleAll(FrontWallCheck.position, GroundRadius, WhatIsGround);
        for (int i = 0; i < frontwalls.Length; i++) 
        {
            if (frontwalls[i].gameObject != gameObject) 
            {
                if (!wasGrounded) {
                    OnWallEvent.Invoke();
                }
                else {
                    OnLandEvent.Invoke();
                }
            }
        }

        Collider2D[] backwalls = Physics2D.OverlapCircleAll(BackWallCheck.position, GroundRadius, WhatIsGround);
        for (int i = 0; i < backwalls.Length; i++) 
        {
            if (backwalls[i].gameObject != gameObject) 
            {
                if (State == "kick" || State == "dive") 
                {
                    OnWallEvent.Invoke();
                }
                else {
                    OnLandEvent.Invoke();
                }
            }
        }

        StateAnimation();
        Move(State);
    }

    private void Attack() 
    {
        Collider2D[] EnemyHitbox = Physics2D.OverlapCircleAll(DivekickAttackPoint.position, AttackRadius, WhatIsEnemy);
        foreach (Collider2D opponent in EnemyHitbox) 
        {
            opponent.gameObject.GetComponent<PlayerController>().ReceiveDamage();
            //rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
            battleManager.BattleWait = true;
            playerdata.Score++;
        }
    }

    private void GroundAttack() 
    {
        Collider2D[] EnemyHitbox = Physics2D.OverlapCircleAll(GroundkickAttackPoint.position, AttackRadius, WhatIsEnemy);
        foreach (Collider2D opponent in EnemyHitbox) 
        {
            opponent.gameObject.GetComponent<PlayerController>().ReceiveDamage();
            //rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
            battleManager.BattleWait = true;
            State = "wait";
            playerdata.Score++;
        }
    }

    public void ReceiveDamage() 
    {
        playerdata.Health = playerdata.Health - (1 - playerdata.Defense);
        if (playerdata.Health <= 0) 
        {
            StartCoroutine(Death());
            //Death();
        }
    }

    private void Move(string State) 
    {
        if (Grounded && State == "startdive") 
        {
            Grounded = false;
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            rgbd.velocity = new Vector2(0f, transform.localScale.y * playerdata.DiveForce);
        }
        if (Grounded && State == "startkick") 
        {
            Grounded = false;
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            rgbd.velocity = new Vector2(transform.localScale.x * playerdata.KickForceBack, transform.localScale.y * playerdata.KickForceUp);
        }
        if (!Grounded && State == "divekick") 
        {
            rgbd.gravityScale = 0f;
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            rgbd.velocity = new Vector2(transform.localScale.x * playerdata.DivekickForceForward, transform.localScale.y * playerdata.DivekickForceDown);
        }
        if (Grounded && State == "groundspecial") 
        {
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            rgbd.velocity = new Vector2(transform.localScale.x * playerdata.DivekickForceForward, transform.localScale.y * playerdata.DashForceUp);
        }
        if (Grounded && State == "groundblock") 
        {
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            StartCoroutine(Block());
        }
        if (!Grounded && State == "airspecial") 
        {
            kickPressed = false;
            divePressed = false;
            kickPressed2 = false;
            divePressed2 = false;
            rgbd.gravityScale = 0f;
            rgbd.velocity = new Vector2(transform.localScale.x * (playerdata.DivekickForceForward*1.5f), transform.localScale.y * (playerdata.DivekickForceDown*1.5f));
        }

        if (Grounded) 
        {
            Flip();
        }
    }

    private IEnumerator Block()
    {
        isBlock = true;
        playerdata.Defense = 1;
        Shield.SetActive(true);
        State = "idle";
        yield return new WaitForSeconds(2f);
        playerdata.Defense = 0;
        Shield.SetActive(false);
        isBlock = false;
    }

    private IEnumerator FactorMode() 
    {
        Factor = true;
        float BonusForceForward = Mathf.Round(OriginalDivekickForward * 1.2f);
        float BonusForceDown = Mathf.Round(OriginalDivekickDown * 1.2f);
        float BonusDiveUp = Mathf.Round(OriginalDiveUp * 1.2f);
        float BonusKickBack = Mathf.Round(OriginalKickBack * 1.2f);
        while (playerdata.CurrentKickMeter > 0) 
        {
            playerdata.CurrentKickMeter -= playerdata.FactorConsumption;
            playerdata.DivekickForceForward = BonusForceForward;
            playerdata.DivekickForceDown = BonusForceDown;
            playerdata.DiveForce = BonusDiveUp;
            playerdata.KickForceBack = BonusKickBack;
            yield return new WaitForSeconds(0.1f);      
        }
        playerdata.DivekickForceForward = OriginalDivekickForward;
        playerdata.DivekickForceDown = OriginalDivekickDown;
        playerdata.DiveForce = OriginalDiveUp;
        playerdata.KickForceBack = OriginalKickBack;
        Factor = false;
    }

    private void StateAnimation() 
    {
        animator.SetBool("Grounded", Grounded);
        if (State == "walldive")
        {
            Hitbox.isTrigger = false;
            animator.SetBool("kick", true);
        }
        if (State == "idle") 
        {
            Hitbox.isTrigger = false;
        }
        if (State == "startkick") 
        {
            animator.SetBool("kick", true);
            Hitbox.isTrigger = true;
        } 
        else 
        {
            animator.SetBool("kick", false);
        }

        if (State == "startdive") 
        {
            animator.SetBool("dive", true);
            Hitbox.isTrigger = true;
        } 
        else 
        {
            animator.SetBool("dive", false);
        }

        if (State == "divekick") 
        {
            animator.SetBool("divekick", true);
            Hitbox.isTrigger = true;
            Attack();
        } 
        else 
        {
            animator.SetBool("divekick", false);
        }

        if (State == "groundspecial") 
        {
            animator.SetBool("groundspecial", true);
            Hitbox.isTrigger = true;
            GroundAttack();
        } 
        else 
        {
            animator.SetBool("groundspecial", false);
        }

        if (State == "airspecial") 
        {
            animator.SetBool("airspecial", true);
            Hitbox.isTrigger = true;
            Attack();
        } 
        else 
        {
            animator.SetBool("airspecial", false);
        }
        if (State == "airspecial" || State == "groundspecial" || Factor == true)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
        }
        if (Factor == true) 
        {
            FactorFire.SetActive(true);
        } 
        else 
        {
            FactorFire.SetActive(false);
        }
    }

    private IEnumerator Death() 
    {
        State = "death";
        Debug.Log("deadge");
        Hitbox.enabled = false;
        animator.SetTrigger("death");
        enemy.GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(battleManager.RoundEndAnnouncement());
        yield return new WaitForSeconds(2f);
    }

}



