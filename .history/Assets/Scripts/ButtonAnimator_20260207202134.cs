using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayPress()
    {
        anim.Play("ButtonPress", 0, 0f);
    }
}
