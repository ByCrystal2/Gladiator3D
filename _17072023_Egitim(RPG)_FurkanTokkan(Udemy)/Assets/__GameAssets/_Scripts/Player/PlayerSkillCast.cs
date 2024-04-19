using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("Mana Settings")]
    public float maxMana = 100f;
    public float totalMana = 100f;
    public float manaRegenSpeed = 2f;
    public Image manaBar;
    
    public bool[] checkOutOfManaActives = new bool[] { true, true, true, true, true, true };

    [Header("CoolDown Icons")]
    public Image[] coolDownIcons;

    [Header("Out Of Mana Icons")]
    public Image[] outOfManaIcons;

    [Header("CoolDown Times")]
    public float[] coolDownTimes;

    [Header("Mana Amounts")]
    public float[] skillsManaAmounts;

    [Header("Required Level")]
    public int Skill1 = 5;
    public int Skill2 = 10;
    public int Skill3 = 15;
    public int Skill4 = 20;
    public int Skill5 = 25;
    public int Skill6 = 30;
   

    

    private bool faded = false;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };
    private Animator animator;

    private bool canAttack = true;

    private PlayerOnClick playerOnClick;

    private List<int> levelList = new List<int>();

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        animator = GetComponent<Animator>();
        playerOnClick = GetComponent<PlayerOnClick>();
        manaBar = GameObject.Find("ManaOrb").GetComponent<Image>();        
        manaBar.fillAmount = totalMana;
    }

    void Start()
    {
        AddLevel();
    }

    
    void Update()
    {
        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        //Geçiþ sürecinde ise ve aktif animasyon idle ise.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TurnThePlayer();
        }
        CheckLevel();
        ManaRegenControl();
        CheckToMana();
        CheckInput();
        CheckToFade();
    }

    private void AddLevel()
    {
        levelList.Add(Skill1);
        levelList.Add(Skill2);
        levelList.Add(Skill3);
        levelList.Add(Skill4);
        levelList.Add(Skill5);
        levelList.Add(Skill6);
    }

    void CheckInput()
    {
        int currentAttackAnimIndex = animator.GetInteger("Attack");
        if (currentAttackAnimIndex == 0)
        {   
            playerOnClick.SetFinishedMovement(false);
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerOnClick.SetFinishedMovement(true);
            }
                
        }

        //Skill Input
        if (Input.GetKeyDown(KeyCode.Alpha1) && levelManager.GetLevel >= Skill1)
        {
            if (!checkOutOfManaActives[0])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[0] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[0];
                UpdateManaAmount(totalMana);
                fadeImages[0] = 1;
                animator.SetInteger("Attack", 1);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && levelManager.GetLevel >= Skill2)
        {
            if (!checkOutOfManaActives[1])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[1] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[1];
                UpdateManaAmount(totalMana);
                fadeImages[1] = 1;
                animator.SetInteger("Attack", 2);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && levelManager.GetLevel >= Skill3)
        {
            if (!checkOutOfManaActives[2])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[2] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[2];
                UpdateManaAmount(totalMana);
                fadeImages[2] = 1;
                animator.SetInteger("Attack", 3);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && levelManager.GetLevel >= Skill4)
        {
            if (!checkOutOfManaActives[3])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[3] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[3];
                UpdateManaAmount(totalMana);
                fadeImages[3] = 1;
                animator.SetInteger("Attack", 4);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) && levelManager.GetLevel >= Skill5)
        {
            if (!checkOutOfManaActives[4])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[4] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[4];
                UpdateManaAmount(totalMana);
                fadeImages[4] = 1;
                animator.SetInteger("Attack", 5);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6) && levelManager.GetLevel >= Skill6)
        {
            if (!checkOutOfManaActives[5])
            {
                return; //Mana eksik.
            }
            playerOnClick.SetTargetPosition(transform.position);
            if (playerOnClick.GetFinishedMovement() && fadeImages[5] != 1 && canAttack)
            {
            totalMana -= skillsManaAmounts[5];
                UpdateManaAmount(totalMana);
                fadeImages[5] = 1;
                animator.SetInteger("Attack", 6);
            }
        }

        else
        {
            animator.SetInteger("Attack", 0);
        }
    }
    void CheckToFade()
    {
        for (int i = 0; i < coolDownIcons.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(coolDownIcons[i], coolDownTimes[i]))
                {
                        fadeImages[i] = 0;
                }
            }
        }
    }
    void CheckLevel()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (levelManager.GetLevel < levelList[i])
            {
                outOfManaIcons[i].gameObject.SetActive(true);
            }
        }
    }
    private void CheckToMana()
    {
        int checkIndex = 0;
        foreach (var amount in skillsManaAmounts)
        {
            if (levelManager.GetLevel >= levelList[checkIndex])
            {
                if (totalMana < amount)
                {
                    outOfManaIcons[checkIndex].gameObject.SetActive(true);
                    checkOutOfManaActives[checkIndex] = false;
                }
                else
                {
                    outOfManaIcons[checkIndex].gameObject.SetActive(false);
                    checkOutOfManaActives[checkIndex] = true;
                }
            }
            checkIndex++;
        }

    }
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }
        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }
        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }
    private void UpdateManaAmount(float  amount)
    {
        manaBar.fillAmount = amount/maxMana;
    }

    private void ManaRegenControl()
    {
        if (totalMana < maxMana)
        {
        totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana / maxMana;
        }
    }

    private void TurnThePlayer()
    {
        Vector3 targetPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos-transform.position), playerOnClick.turnSpeed * Time.deltaTime);
    }
}
