using UnityEngine;

public class NumberButton : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // This function ONLY plays animation
    public void PlayPressAnimation()
    {
        if (anim != null)
        {
            anim.Play("ButtonPress", 0, 0f);
        }
    }
}
