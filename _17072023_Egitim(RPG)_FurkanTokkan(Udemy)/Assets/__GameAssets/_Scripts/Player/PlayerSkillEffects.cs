using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEffects : MonoBehaviour
{
    
    [Header("Skill Effects")]
    public GameObject HammerSkill;
    public GameObject KickSkill;
    public GameObject SpellCastSkill;
    public GameObject HealSkill;
    public GameObject ShieldSkill;
    public GameObject ComboSkill;

    [Header("Skill Transforms")]
    public Transform HammerTransform;
    public Transform SpellCastTransform;    
    public Transform KickTransform;
    public Transform ShieldTransform;
    public Transform HealTransform;
    public Transform ComboSkillTransform;


    private void GetHammerSkill()
    {
        Instantiate(HammerSkill, HammerTransform.position, Quaternion.identity);
    }

    private void GetKickSkill()
    {
        Instantiate(KickSkill, KickTransform.position, Quaternion.identity);
    }

    private void GetSpellCastSkill()
    {
        Instantiate(SpellCastSkill, SpellCastTransform.position, Quaternion.identity);
    }   

    private void GetComboSkill()
    {
        Instantiate(ComboSkill, ComboSkillTransform.position, Quaternion.identity);
    }

    private void ShieldCast()
    {
        GameObject ShieldClone = Instantiate(ShieldSkill, ShieldTransform.position, Quaternion.identity);
        ShieldClone.transform.SetParent(transform);
    }

    private void HealCast()
    {
       GameObject HealClone = Instantiate(HealSkill, HealTransform.position, Quaternion.identity);
       HealClone.transform.SetParent(transform);
    }

}
