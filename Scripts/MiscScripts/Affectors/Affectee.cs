using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affectee : MonoBehaviour
{
    [Header("What to listen to")]
    public Affector affector;

    [Header("Simple Toggle SetActive()")]
    public GameObject[] simpleAffect;

    [Header("Simple Animator Trigger")]
    public Animator animator;
    public string animationTrigger;
    public string animationUntrigger;

    public virtual void Start()
    {
        if (!affector)
        {
            Debug.LogError("No affector to listen to", gameObject);
        }

        affector.RegisterTrigger(ListenTrigger);
        affector.RegisterUntrigger(ListenUntrigger);
    }

    public virtual void ListenTrigger()
    {
        InvertActives();

        animator.SetTrigger(animationTrigger);
    }

    public virtual void ListenUntrigger()
    {
        InvertActives();

        animator.SetTrigger(animationUntrigger);
    }

    private void InvertActives()
    {
        for (int i = 0; i < simpleAffect.Length; i++)
        {
            simpleAffect[i].SetActive(!simpleAffect[i].activeInHierarchy);
        }
    }
}
