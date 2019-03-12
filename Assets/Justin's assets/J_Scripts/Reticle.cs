using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private Vector3 mc;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mc = Input.mousePosition;
        mc = Camera.main.ScreenToWorldPoint(mc);
        Vector3 m = new Vector3(mc.x, mc.y, 0.0f);
        this.transform.position = m;

    }
}