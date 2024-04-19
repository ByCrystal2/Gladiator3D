using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private float currentExp;
    private int currentLevel;
    private int expToNextLevel;
    public static LevelManager instance;

    public int GetLevel { get { return currentLevel + 1; } }

    [Header("UI Proparties")]
    public Image expBar;
    public TextMeshProUGUI txtLevel;


    [Header("Level Effects")]
    public GameObject LevelEffect;
    public Transform LevelEffectTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
            Destroy(gameObject);

        expBar.fillAmount = 0;
        currentLevel = 0;
        currentExp = 0;
        expToNextLevel = 100;

        UpdateLevelText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddExp(Random.Range(0,20));
            Debug.Log(currentLevel);
        }
    }

    void UpdateLevelText()
    {
        txtLevel.text = GetLevel.ToString();
    }
    public void AddExp(float amount)
    {
        currentExp += amount;
        expBar.fillAmount = (float)currentExp / expToNextLevel;
        if (currentExp >= expToNextLevel)
        {
            currentLevel++;
            GetPlayerLevelUpEffect();
            UpdateLevelText();
            currentExp -= expToNextLevel;
            expBar.fillAmount = 0f;
        }
    }

    private void GetPlayerLevelUpEffect()
    {
        Instantiate(LevelEffect, LevelEffectTransform.position, Quaternion.identity);
        Destroy(LevelEffect, 1f);
    }

    private void OnEnable()
    {
        EnemyHealth.onDeath += AddExp;
    }
    private void OnDisable()
    {
        EnemyHealth.onDeath -= AddExp;
    }
}
