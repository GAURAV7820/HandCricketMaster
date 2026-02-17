using UnityEngine;

public class Screenshot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("PlayStoreShot.png", 1);
            Debug.Log("Screenshot Saved!");
        }
    }
}
