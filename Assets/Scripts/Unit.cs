using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dugeon.Signal.SignalDefine;

public class Unit : MonoBehaviour
{
    public int HealtPointMax;
    public int CurrentHP;
    public int Damage;
    public int speed;

    [SerializeField] private string _nameUnit;
    [SerializeField] private int _level;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip clipInfo;
    public Transform InitalTransform;
    public void SlideToTarget(Unit target)
    {
        //add anime to this
        //play anim and attack
        transform.position = (target.transform.position);

        //StartCoroutine(WaitSlide(target));
        //Debug.Log("SlideToTarget");
    }
    public IEnumerator WaitSlide(Unit target)
    {
        yield return new WaitForSeconds(1f);

    }
    public void SlideToBack(Transform initialPosion)
    {
        //play anime
        StartCoroutine(WaitAttack(initialPosion));
    }
    public IEnumerator WaitAttack(Transform initialPosion)
    {
        // yield return new WaitUntil(() => !AnimatorIsPlaying("attack"));
        yield return new WaitForSeconds(0.2f);
        transform.position = (initialPosion.transform.position);
    }

    public int TakeDamage(int damage)
    {
        CurrentHP -= damage;
        return CurrentHP;

    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying("attack") && _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
