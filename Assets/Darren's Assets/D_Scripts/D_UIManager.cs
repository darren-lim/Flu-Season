using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_UIManager : MonoBehaviour
{
    public TextMeshProUGUI Reputation;
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI Gun;
    public TextMeshProUGUI Invincibility;
    public TextMeshProUGUI EnemyLeft;
    public TextMeshProUGUI GOScore;
    //public TextMeshProUGUI HighGOScore;
    public GameObject PauseCanvas;
    public GameObject GameOverCanvas;
    public bool paused;
    public Shoot shootScript;
    public GameObject[] Hearts; // 5 HEARTS

    private int LifeStore; // keep track of life before changes

    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(false);
        D_SimpleLevelManager.current.OnTakeDamage.AddListener(UpdateLives);
        D_SimpleLevelManager.current.OnGameOver.AddListener(GameOver);
        LifeStore = D_SimpleLevelManager.current.playerLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shootScript != null)
        {
            PauseOrResume();
        }
        UpdateGun();
        UpdateDodge();
    }

    public void UpdateLives()
    {
        int lives = D_SimpleLevelManager.current.playerLives;
        if (lives < LifeStore)
        {
            Hearts[LifeStore - 1].SetActive(false);
        }
        else if (lives > LifeStore)
        {
            Hearts[lives - 1].SetActive(true);
        }
        LifeStore = lives;
    }
    public void UpdateScore()
    {
        Reputation.text = string.Format("Reputation: {0}", D_SimpleLevelManager.current.Score);
        EnemyLeft.text = string.Format("Enemies Left: {0}", D_SimpleLevelManager.current.EnemiesLeft);
    }
    public void GameOver()
    {
        GameOverCanvas.gameObject.SetActive(true);
        if (shootScript != null)
            shootScript.enabled = false;
        GOScore.text = string.Format("Final Reputation: {0}", D_SimpleLevelManager.current.Score);
        //HighGOScore.text = string.Format("Highest Reputation: {0}", D_SimpleLevelManager.current.Score);
    }

    public void UpdateGun()
    {
        Gun.text = Shoot.current.gunAmmoUIStr;
    }

    public void UpdateDodge()
    {
        if (!D_PlayerTestScript.current.dodging)
            Invincibility.text = "Activate Dodge";
        else
            Invincibility.text = "";

    }

    public void PauseOrResume()
    {
        if (shootScript == null && PauseCanvas == null)
            return;
        if (!paused)
        {
            paused = true;
            PauseCanvas.gameObject.SetActive(true);
            PauseCanvas.transform.Find("MainPause").gameObject.SetActive(true);
            PauseCanvas.transform.Find("Options").gameObject.SetActive(false);
            Time.timeScale = 0;
            shootScript.enabled = false; //MAKE THIS A UNITY EVENT THIS IS TEMPORARY FIX
            Cursor.visible = true;
        }
        else
        {
            paused = false;
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            shootScript.enabled = true;
            Cursor.visible = false;
        }
    }
}
