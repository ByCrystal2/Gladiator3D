using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWayPointTracker : MonoBehaviour
{
    [Header("WayPoint")]
    public Vector3[] walkPoints;

    [Header("Movement Settings")]
    public float turnSpeed = 5.0f;
    public float patrolTime = 15.0f;

    public float walkDistance = 8.0f;
    
    [Header("Attack Settings")]
    public float attackDistance = 1.4f;
    public float attackRate = 1f;

    private Transform playerTarget;
    private Animator anim;

    private NavMeshAgent agent;

    private float currentAttackTime;

    private Vector3 nextDestination;
    private int index;

    EnemyHealth eh;
    PlayerHealth ph;
    //Health

    private void Awake()
    {
        
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        eh = GetComponent<EnemyHealth>();
        ph = playerTarget.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        List<GameObject> Spawn = new List<GameObject>();
        foreach (GameObject spawnPoint in SpawnPoints)
        {
            Spawn.Add(spawnPoint);
            
        }
        foreach (var item in Spawn)
        {
            Debug.Log(item.name + "Spawn Transform Noktasý: " + item.transform.position);
        }

        int randomSpawnPointIndex = Random.Range(0, Spawn.Count);
        GameObject currentSpawn = Spawn[randomSpawnPointIndex];
        transform.position = currentSpawn.transform.position;
        SpawnPoint sp = currentSpawn.GetComponent<SpawnPoint>();
        walkPoints = new Vector3[sp.walkPointCount];
        for (int i = 0; i < sp.walkPointCount; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * sp.walkRadius;

            randomDirection += currentSpawn.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, sp.walkRadius, 1);
            Vector3 finalPosition = hit.position;                
            walkPoints[i] = finalPosition;
        }
        Debug.Log(currentSpawn.name);
        Spawn.Remove(currentSpawn);
        
        index = Random.Range(0,walkPoints.Length);

        if (walkPoints.Length > 0)
        {
            InvokeRepeating("Patrol",Random.Range(0,patrolTime),Random.Range(8,patrolTime)); // Bu kod: Patrol adlý methodun, 0 ile patrolTime deðiþkeni arasýnda rastgele bir sayý üretip, o sayý sürecinin sonunda çaðrýlmasýný saðlar. Bundan sonra ki tüm çaðrýlmalar ise 5 ile patronTime deðiþkeni arasýnda rastgele üretilen bir sayý süresince gerçekleþir.
        }
    }
    void Start()
    {
        agent.avoidancePriority = Random.Range(1, 51);
    }

    
    void Update()
    {
        if (!eh.isDead)
        {
            MoveAndAttack();
        }        
    }

    void MoveAndAttack()
    {
            float distance = Vector3.Distance(transform.position, playerTarget.position);

            if (distance > walkDistance) // Oyuncu ile Enemy arasýnda ki mesafe,belirlenen walkDistance aralýðýndan büyükse, Enemy walkPoints noktalarýnda ilerlemeye devam etsin.
            {
                if (agent.remainingDistance >= agent.stoppingDistance) // Bu Kondisyon: Gideceðimiz nokta, bulunduðumuz noktadan büyükmü onu kontrol eder.
                {
                    agent.isStopped = false;
                    agent.speed = 2f;
                    anim.SetBool("Walk", true);

                    nextDestination = walkPoints[index];
                    agent.SetDestination(nextDestination); // Bu kod: Belirtilen noktaya en kolay nasýl ulaþabilir onu hesaplar ve iþler.
                }
                else
                {
                    agent.isStopped = true;
                    agent.speed = 0f;
                    anim.SetBool("Walk", false);

                    nextDestination = walkPoints[index];
                    agent.SetDestination(nextDestination);
                }
            }
            else
            {
                if (distance > attackDistance + 0.15f && ph.currentHealth > 0)
                {
                    if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") /*#1*/ && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f)
                    {
                        anim.ResetTrigger("Attack");
                        agent.isStopped = false;
                        agent.speed = 3f;

                        anim.SetBool("Walk", true);

                        agent.SetDestination(playerTarget.position);

                    }
                }
                else if (distance <= attackDistance && ph.currentHealth > 0) // Enemy Saldýrý Ýþlemleri
                {
                    agent.isStopped = true;
                    anim.SetBool("Walk", false);
                    agent.speed = 0f;

                    Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), turnSpeed * Time.deltaTime);

                    if (currentAttackTime >= attackRate) // Enemy mevcut saldýrý yapma süresinin dolmasý ve saldýrý yapma iþlemi.
                    {
                        anim.SetTrigger("Attack");
                        currentAttackTime = 0;
                    }
                    else // Enemy mevcut saldýrý süresini arttýrma
                    {
                        currentAttackTime += Time.deltaTime;
                    }
                }
            }
        
    }

    void Patrol()
    {
        index = index == walkPoints.Length - 1 ? 0 : index + 1; // index deðiþkeni son yürüme noktasýna eþitse, index'i 1 yap, deðilse index'e 1 ekle.
    }
}
