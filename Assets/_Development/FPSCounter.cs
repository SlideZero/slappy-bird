using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsDisplay;

    private void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + fps;
    }
}
