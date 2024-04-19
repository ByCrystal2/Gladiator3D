using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOnClick : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float turnSpeed = 15f;
    public float attackRange = 2f;

    private Animator animator;
    private CharacterController controller;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private Vector3 playerMove = Vector3.zero;
    private Vector3 targetMovePoint = Vector3.zero;
    private Vector3 targetAttackPoint = Vector3.zero;

    private float currentSpeed;
    private float playerToPointDistance;
    private float gravity = 9.8f;
    private float height;

    private bool canMove;
    private bool canAttackMove;
    private bool finishedMovement;

    private Vector3 newMovePoint;
    private Vector3 newAttackPoint;

    public GameObject EnemySkeletonPrefab;
    private GameObject TargetEnemy;
    private void Awake()
    {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
        currentSpeed = maxSpeed;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        CalculadeHeight();
        CheckIfFinishedMovement();
        AttackMove();
        if (Input.GetKeyDown(KeyCode.S)) 
        { 
            Instantiate(EnemySkeletonPrefab, transform.position + Vector3.forward, Quaternion.identity);
        }
    }

    private bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true: false;
    }

    void AttackMove()
    {
        if (canAttackMove)
        {
            targetAttackPoint = TargetEnemy.gameObject.transform.position;

            newAttackPoint = new Vector3(targetAttackPoint.x , transform.transform.position.y, targetAttackPoint.z);
        }
        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), turnSpeed * 2 * Time.deltaTime);
        }
    }

    private void CalculadeHeight()
    {
        if (IsGrounded())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    private void CheckIfFinishedMovement()
    {
        if (!finishedMovement)
        {
            Debug.Log("Hareket onaylandý.");
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") /*#1*/ && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                Debug.Log("Hareket ediyor.");
                finishedMovement = true;
            }
        }
        else
        {
            MovePlayer();
            playerMove.y = height * Time.deltaTime;
            collisionFlags = controller.Move(playerMove);
        }
    }

    private void MovePlayer()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (playerToPointDistance >= 1.0f)
                    {
                        canMove = true;
                        canAttackMove = false;
                        targetMovePoint = hit.point;
                    }
                }

                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    if (playerToPointDistance >= 1.0f)
                    {
                        TargetEnemy = hit.collider.gameObject.GetComponentInParent<EnemyWayPointTracker>().gameObject;
                        canMove = true;
                        canAttackMove = true;
                    }
                }


            }
        }
        if (canMove)
        {
            animator.SetFloat("Speed", 1.0f);

            if (!canAttackMove)
            {
                newMovePoint = new Vector3(targetMovePoint.x,transform.position.y, targetMovePoint.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newMovePoint - transform.position), turnSpeed*Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), turnSpeed * Time.deltaTime);
            }

            playerMove = transform.forward * currentSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position,newMovePoint) <= 0.6f && !canAttackMove)
            {
                canMove = false;
                canAttackMove = false;
            }
            else if (canAttackMove && !TargetEnemy.GetComponent<EnemyHealth>().isDead)
            {
                if (Vector3.Distance(transform.position,newAttackPoint) <= attackRange)
                {
                    playerMove.Set(0f, 0f, 0f);
                    animator.SetFloat("Speed", 0f);

                    targetAttackPoint = Vector3.zero;
                    animator.SetTrigger("AttackMove");

                    canAttackMove = false;
                    canMove = false;
                }
            }
        }
        else
        {
            playerMove.Set(0f,0f,0f);
            animator.SetFloat("Speed", 0f);
        }
    }
    public bool GetFinishedMovement(){ return finishedMovement; }
    public void SetFinishedMovement(bool finishedMovement) { this.finishedMovement = finishedMovement; }

    public bool GetCanMove(){ return canMove; }
    public void SetCanMove(bool canMove) { this.canMove = canMove; }

    public Vector3 GetTargetPosition(){return targetMovePoint;}
    public void SetTargetPosition(Vector3 targetPosition){this.targetMovePoint = targetPosition;}

    public float GetTurnSpeed(){return turnSpeed;}
    public void SetTurnSpeed(float turnSpeed)    {this.turnSpeed = turnSpeed;}
}

#region KOD ACÝKLAMA DETAYLARÝ
       /*
         #1: Mevcut animatorün aktif olan layer 0'da ki (Ýlk çalýþan animasyon) animasyonun ismi "Idle"'mi diye sorar, doðruysa true döndürür. //Not: Normalde bu kýsýmda eðitimden aldýðým bilgilere göre kodun baþýnda ! olmasý gerekiyordu...
        
        
        
        
        
        
        */
#endregion
