using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickResponseBox : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("waitFadeOut");
    }
    IEnumerator waitFadeOut()
    {
        yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return null;
        Destroy(this.gameObject);
    }
}
