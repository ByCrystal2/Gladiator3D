using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : PlayerSkillDamage
{
    public GameObject Explotion;

    public float Speed = 10f;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }


    internal override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        if (colided)
        {
            Instantiate(Explotion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
