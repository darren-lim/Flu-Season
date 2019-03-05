using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PlayerTestScript : MonoBehaviour
{
    [System.NonSerialized] public static D_PlayerTestScript current;

    // Player specific properties
    [SerializeField] private float speed = 5f;
    [System.NonSerialized] public int lives = 3; 

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


    private D_SimpleLevelManager LManager;

    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !invincible)
        {
            if (lives == 1)
            {
                Debug.Log("GAME OVER!");
                Destroy(this.gameObject);
            }
            else
                lives--;
            StartCoroutine(GenIFrames());
        }
        LManager.playerLives = lives;
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
    }

    private IEnumerator GenIFrames()
    {
        BecomeInvincible();
        yield return new WaitForSecondsRealtime(2);
        BecomeInvincible();
    }

    private void BecomeInvincible()
    {
        if (spriteRenderer.sprite == sprite1)
        {
            spriteRenderer.sprite = sprite2;
            invincible = true;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
            invincible = false;
        }
    }
}
