using UnityEngine;

public class Screenshot : MonoBehaviour
{
    int count = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string fileName = "PlayStoreShot_" + count + ".png";
            ScreenCapture.CaptureScreenshot(fileName, 1);
            Debug.Log("Screenshot Saved: " + fileName);

            count++;
        }
    }
}
