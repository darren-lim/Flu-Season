using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PlayerTestScript : MonoBehaviour
{
    [System.NonSerialized] public static D_PlayerTestScript current;

    // Player specific properties
    [SerializeField] private float speed = 5f;
    [System.NonSerialized] public int lives = 5; 

    // Helper variables for boundary
    private Camera mCamera;
    private Rigidbody2D mRigidbody;
    private Collider2D mCollider;

    private double lboundary;
    private double rboundary;
    private double uboundary;
    private double dboundary;

    // Helper variables for dodging/Iframes
    public Sprite sprite1;
    public Sprite sprite2;
    public SpriteRenderer spriteRenderer;
    [System.NonSerialized] public bool invincible = false;
    [System.NonSerialized] public bool dodging = false;

    //blink effect
    public float blinkTimer = 0.0f;
    public float blinkMiniDuration = 0.1f;
    public float blinkTotalTimer = 0.0f;
    public float blinkTotalDuration = 1.0f;
    public bool startBlinking = false;
    

    private D_SimpleLevelManager LManager;

    //animations
    private Animator m_animator;

    void Awake()
    {
        if (current == null)
        {
            current = this;
            m_animator = this.GetComponent<Animator>();
        }
        else
        {
            Destroy(this.gameObject);
        }
            
    }

    void Start()
    {
        // Use Camera to initialize boundaries 
        mCamera = Camera.main;
        lboundary = mCamera.pixelWidth * .10;
        rboundary = mCamera.pixelWidth * .90;
        uboundary = Screen.height * .95;
        dboundary = Screen.height * .05;

        // Initialize player rigidbody & collider
        mRigidbody = this.GetComponent<Rigidbody2D>();
        mCollider = this.GetComponent<Collider2D>();

        // Initialize the spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
        else
            sprite1 = spriteRenderer.sprite;
        

        LManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !dodging && !invincible)
        {
            dodging = true;
            StartCoroutine(SpotDodge());
        }
        if (startBlinking)
        {
            SpriteBlinkingEffect();
        }

    }

    private void Move()
    {
        CheckBoundaries();
    }

    IEnumerator SpotDodge()
    {
        StartCoroutine(GenIFrames());
        yield return new WaitForSecondsRealtime(10);
        dodging = false;
    }

    private bool already = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if ((other.CompareTag("Enemy") || other.CompareTag("EnemyBullet")) && !invincible && !already)
        {
            already = true;
            invincible = true;
            if (lives == 1)
            {
                Debug.Log("GAME OVER!");
                FindObjectOfType<D_AudioManager>().Play("EnemyDeath");
                Destroy(this.gameObject);
            }
            else
            {
                lives--;
                FindObjectOfType<D_AudioManager>().Play("PlayerGetHit");
                startBlinking = true;
            }
                
        }
        LManager.playerLives = lives;
        LManager.PlayerTakeDamage();
        already = false;
    }

    // Helper functions
    private void CheckBoundaries()
    {

        Vector2 playerPos = Camera.main.WorldToScreenPoint(mRigidbody.position);
        mRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);

        if (playerPos.x < lboundary)
            mRigidbody.velocity = new Vector2(Mathf.Max(0, mRigidbody.velocity.x),mRigidbody.velocity.y);
        if (playerPos.x > rboundary)
            mRigidbody.velocity = new Vector2(Mathf.Min(0, mRigidbody.velocity.x), mRigidbody.velocity.y);
        if (playerPos.y > uboundary)
            mRigidbody.velocity = new Vector2(mRigidbody.velocity.x, Mathf.Min(0, mRigidbody.velocity.y));
        if (playerPos.y < dboundary)
            mRigidbody.velocity = new Vector2(mRigidbody.velocity.x, Mathf.Max(0, mRigidbody.velocity.y));

        m_animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed));
        if (mRigidbody.velocity.x > 0)
        {
            m_animator.SetBool("Right", true);
            m_animator.SetBool("Left", false);
        }
        if (mRigidbody.velocity.x < 0)
        {
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Left", true);
        }
        if (mRigidbody.velocity.x == 0)
        {
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Left", false);
        }
        if (mRigidbody.velocity.y > 0)
        {
            m_animator.SetBool("Up", true);
            m_animator.SetBool("Down", false);
        }
        if (mRigidbody.velocity.y < 0)
        {
            m_animator.SetBool("Up", false);
            m_animator.SetBool("Down", true);
        }
        if (mRigidbody.velocity.y == 0)
        {
            m_animator.SetBool("Up", false);
            m_animator.SetBool("Down", false);
        }

    }

    private IEnumerator GenIFrames()
    {
        BecomeInvincible();
        yield return new WaitForSecondsRealtime(2);
        BecomeInvincible();
    }

    private void BecomeInvincible()
    {
        if (!invincible)
        {
            m_animator.SetBool("Dodge", true);
            invincible = true;
        }
        else
        {
            m_animator.SetBool("Dodge", false);
            invincible = false;
        }

        
    }
    private void SpriteBlinkingEffect()
    {
        blinkTotalTimer += Time.deltaTime;
        if (blinkTotalTimer >= blinkTotalDuration)
        {
            startBlinking = false;
            blinkTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            invincible = false;

            return;
        }
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkMiniDuration)
        {
            blinkTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                invincible = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
