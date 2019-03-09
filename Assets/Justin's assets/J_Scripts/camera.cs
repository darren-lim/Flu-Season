using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // Switch to 640 x 480 full-screen
        //Screen.SetResolution(800, 600, false);
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
            transform.position = player.transform.position + offset;
    }
}
