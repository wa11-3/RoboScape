using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer rende;

    //[SerializeField] float num = 0;

    [SerializeField] private bool isAlive;
    [SerializeField] private bool isAnti;
    private int trapsLayer;

    RaycastHit2D groundHitUp1;
    RaycastHit2D groundHitDown1;
    RaycastHit2D groundHitUp2;
    RaycastHit2D groundHitDown2;
    RaycastHit2D doorHit;
    RaycastHit2D infoHit;

    public ManagerScript manager;

    public bool winner;

    public int lives = 3;
    public GameObject[] gamelives = new GameObject[4];

    [SerializeField] private AudioClip[] audios = new AudioClip[4];
    AudioSource musicPlayer;

    float delay = 0.6f;
    float timestamp = 0.0f;

    float coyoteTime = 0.2f;
    float coyoteCounter;

    public GameObject info;
    public Sprite info1;
    public Sprite info2;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rende = GetComponent<SpriteRenderer>();
        trapsLayer = LayerMask.NameToLayer("Traps");
        isAlive = true;
        isAnti = false;
        winner = false;
        lives = 3;
        musicPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAlive)
        {
            HorizontalMove();
            Gravity();
            RayCasters();
            DoorCheck();
            InfoCheck();
        }
        LiveCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trapsLayer && isAlive)
        {
            StopAllCoroutines();
            StopCoroutine(AniGrav());
            if (lives >= 1)
            {
                isAnti = false;
                anim.Play("Dead");
                musicPlayer.clip = audios[3];
                musicPlayer.Play();
                lives -= 1;
                rigid.velocity = new Vector2(0.0f, 0.0f);
                StartCoroutine(AnimDead());
            }
            else
            {
                anim.Play("Dead");
                isAlive = false;
                rigid.gravityScale = 0;
                rigid.velocity = new Vector2(0.0f, 0.0f);
                StartCoroutine(manager.GameOver());
            }

            if (transform.rotation.eulerAngles.z >= 180.0f)
            {
                isAnti = false;
                transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
            }
        }

    }

    IEnumerator AniGrav()
    {
        anim.Play("AntiGravity");
        yield return new WaitForSeconds(0.5f);
        transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
        anim.Play("Idle");
    }

    IEnumerator AnimDead()
    {
        isAlive = false;
        rigid.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isAlive = true;
        anim.Play("Idle");
        rigid.gravityScale = 1;
        transform.position = new Vector3(-8.486f, -3.355f, 0.0f);
    }

    void HorizontalMove()
    {
        float horizontalIn = Input.GetAxisRaw("Horizontal");
        if ((horizontalIn < 0 && !isAnti) || (horizontalIn > 0 && isAnti))
            rende.flipX = true;
        else if ((horizontalIn > 0 && !isAnti) || (horizontalIn < 0 && isAnti))
            rende.flipX = false;
        anim.SetFloat("Move", Mathf.Abs(horizontalIn));
        rigid.velocity = new Vector2(horizontalIn * 3.0f, rigid.velocity.y);
    }

    void Gravity()
    {
        if (groundHitDown1.collider != null || groundHitUp1.collider != null || groundHitDown2.collider != null || groundHitUp2.collider != null)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown("space") && (coyoteCounter > 0) && Time.time >= timestamp)
        {
            timestamp = Time.time + delay;
            if (!isAnti)
            {
                isAnti = true;
                rigid.gravityScale = -2;
                musicPlayer.clip = audios[0];
                musicPlayer.Play();
                StartCoroutine(AniGrav());
            }
            else
            {
                isAnti = false;
                rigid.gravityScale = 2;
                musicPlayer.clip = audios[1];
                musicPlayer.Play();
                StartCoroutine(AniGrav());
            }
        }

        if (Input.GetKeyDown("space"))
        {
            coyoteCounter = 0;
        }
    }

    void RayCasters()
    {
        groundHitUp1 = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0, 0), Vector2.up, 0.7f, 1 << 7);
        groundHitDown1 = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0, 0), Vector2.down, 0.7f, 1 << 7);
        groundHitUp2 = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0, 0), Vector2.up, 0.7f, 1 << 7);
        groundHitDown2 = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0, 0), Vector2.down, 0.7f, 1 << 7);
        doorHit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, 1 << 8);
        infoHit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, 1 << 9);
        Debug.DrawRay(transform.position + new Vector3(0.3f, 0, 0), Vector2.down * 0.7f, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.3f, 0, 0), Vector2.up * 0.7f, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-0.3f, 0, 0), Vector2.down * 0.7f, Color.yellow);
        Debug.DrawRay(transform.position + new Vector3(-0.3f, 0, 0), Vector2.up * 0.7f, Color.magenta);
    }

    void DoorCheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && doorHit.collider != null && (LightCheck()))
        {
            print("Win");
            StartCoroutine(manager.Win());
        }
    }

    bool LightCheck()
    {
        foreach (bool light in manager.lights)
        {
            if (!light)
            {
                return false;
            }
        }
        return true;
    }

    void InfoCheck()
    {
        if (infoHit.collider != null)
        {
            if (infoHit.collider.name == "Info1")
            {
                info.GetComponent<Image>().sprite = info1;
                info.SetActive(true);
            }
            if (infoHit.collider.name == "Info2")
            {
                info.GetComponent<Image>().sprite = info2;
                info.SetActive(true);
            }          
        }
      
        else
            info.SetActive(false);
    }

    void LiveCheck()
    {
        switch (lives)
        {
            case 3:
                gamelives[0].SetActive(true);
                gamelives[1].SetActive(true);
                gamelives[2].SetActive(true);
                break;

            case 2:
                gamelives[0].SetActive(false);
                gamelives[1].SetActive(true);
                gamelives[2].SetActive(true);
                break;

            case 1:
                gamelives[0].SetActive(false);
                gamelives[1].SetActive(false);
                gamelives[2].SetActive(true);
                break;

            case 0:
                gamelives[0].SetActive(false);
                gamelives[1].SetActive(false);
                gamelives[2].SetActive(false);
                break;
        }
    }
}
