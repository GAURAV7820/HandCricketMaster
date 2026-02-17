using UnityEngine;

public class NumberButton : MonoBehaviour
{
    public int number;
    public GameManager gameManager;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnClick()
    {
        if (anim != null)
            anim.Play("ButtonPress", 0, 0f);

        gameManager.OnNumberPressed(number);
    }
}
