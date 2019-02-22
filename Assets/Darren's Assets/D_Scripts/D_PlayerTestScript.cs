using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PlayerTestScript : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //need to check direction of movement, to rotate player to look towards direction
        //8 directions, N NW W SW S SE E NE
        Move();
    }

    private void Move()
    {
        this.transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Input.GetAxis("Vertical") * Speed * Time.deltaTime, 0);
    }
}
