using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    /*[HideInInspector]*/ public float currentHealth;
    public float maxHealth = 100f;
    private bool isShielded;
    public bool Shielded { get { return isShielded; } set { isShielded = value; } }
    private Animator anim;

    private Image healtImage;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healtImage = GameObject.Find("HealthOrb").GetComponent<Image>();
    }

    public void TakeDamage(float amount)
    {
        if (!isShielded)
        {
            currentHealth -= amount;
            UpdateHealth();

            if (currentHealth <= 0f)
            {
                anim.SetBool("Death", true);
            }
        }
    }

    public void TakeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }
    
    public void UpdateHealth()
    {
        healtImage.fillAmount = currentHealth / maxHealth;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TakeDamage(5);
            print("Current Player Heal: " + currentHealth);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TakeHealth(5);
            print("Current Player Heal: " + currentHealth);
        }
    }




}
