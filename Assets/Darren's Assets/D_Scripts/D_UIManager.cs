using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_UIManager : MonoBehaviour
{
    public TextMeshProUGUI Reputation;
    public TextMeshProUGUI Lives;

    // Start is called before the first frame update
    void Start()
    {
        D_SimpleLevelManager.current.OnTakeDamage.AddListener(UpdateLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives()
    {
        Lives.text = string.Format("Lives: {0}", D_SimpleLevelManager.current.playerLives);
    }
    public void UpdateScore()
    {
        Reputation.text = string.Format("Reputation: {0}", D_SimpleLevelManager.current.Score);
    }
}
