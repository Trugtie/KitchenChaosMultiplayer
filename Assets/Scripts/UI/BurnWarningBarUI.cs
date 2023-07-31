using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnWarningBarUI : MonoBehaviour
{
    private const string IS_WARNING = "IsWarning";
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool isWarning = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool(IS_WARNING, isWarning);
    }
}
