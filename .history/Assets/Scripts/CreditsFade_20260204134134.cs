using UnityEngine;

public class CreditsFade : MonoBehaviour
{
    public float speed = 1.2f;
    CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (cg.alpha < 1)
            cg.alpha += Time.deltaTime * speed;
    }
}