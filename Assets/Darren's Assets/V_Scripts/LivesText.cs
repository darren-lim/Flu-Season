using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class LivesText : MonoBehaviour
{
    public D_PlayerTestScript playerComponent;
    TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        livesText = this.GetComponent<TextMeshProUGUI>();
        playerComponent = GameObject.Find("Player").GetComponent<D_PlayerTestScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null)
            livesText.text = "Lives: 0";
        else
            livesText.text = string.Format("Lives: {0}", playerComponent.lives);
    }
}
