using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PlayerTestScript : MonoBehaviour
{
    // Prefabs
    public List<GameObject> VaccinePrefabs;
    

    // Player specific properties
    [SerializeField] private float speed = 5f;
    private float vaccineSpeed = 8f;
    [System.NonSerialized] public int lives = 3; 
    private int direction = 5; // 1 = E, 2 = NE, 3 = N, 4 = NW, 
                               // 5 = W, 6 = SW, 7 = S, 8 = W

    // Misc. helper variables
    private Camera mCamera;
    private Rigidbody2D mRigidbody;
    private Collider2D mCollider;

    private double lboundary;
    private double rboundary;
    private double uboundary;
    private double dboundary;

    private D_SimpleLevelManager LManager;

    // Start is called before the first frame update
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

        LManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //need to check direction of movement, to rotate player to look towards direction
        //8 directions, N NW W SW S SE E NE
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    private void Move()
    {
        CheckBoundaries();
        //GetDirection();
        //PrintDirection();
    }

    void Shoot()
    {
        GameObject current_vaccine = Instantiate(VaccinePrefabs[direction-1], this.transform.position, Quaternion.identity);
        current_vaccine.GetComponent<Rigidbody2D>().velocity = GetVaccVelocity();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (lives == 1)
            {
                Debug.Log("GAME OVER!");
                Destroy(this.gameObject);
            }
            else
                lives--;
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

    private void GetDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            direction = 2;
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            direction = 4;
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            direction = 6;
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            direction = 8;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction = 1;
        else if (Input.GetKey(KeyCode.UpArrow))
            direction = 3;
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = 5;
        else if (Input.GetKey(KeyCode.DownArrow))
            direction = 7;
    }

    Vector2 GetVaccVelocity()
    {
        switch (direction)
        {
            case 1:
                return Vector2.right * vaccineSpeed;
            case 2:
                return new Vector2(1,1) * vaccineSpeed;
            case 3:
                return Vector2.up * vaccineSpeed;
            case 4:
                return new Vector2(-1, 1) * vaccineSpeed;
            case 5:
                return Vector2.left * vaccineSpeed;
            case 6:
                return new Vector2(-1, -1) * vaccineSpeed;
            case 7:
                return Vector2.down * vaccineSpeed;
            case 8:
                return new Vector2(1, -1) * vaccineSpeed;
            default:
                Debug.Log("ERROR: Something is wrong with direction!");
                break;
        }

        return new Vector2(0,0);
    }

    // Debugging Functions
    private void PrintDirection()
    {
        switch (direction)
        {
            case 1:
                Debug.Log("E");
                break;
            case 2:
                Debug.Log("NE");
                break;
            case 3:
                Debug.Log("N");
                break;
            case 4:
                Debug.Log("NW");
                break;
            case 5:
                Debug.Log("W");
                break;
            case 6:
                Debug.Log("SW");
                break;
            case 7:
                Debug.Log("S");
                break;
            case 8:
                Debug.Log("SE");
                break;
            default:
                Debug.Log("ERROR: Something is wrong with direction!");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            mRigidbody.velocity = Vector3.zero;
        }
    }
}
