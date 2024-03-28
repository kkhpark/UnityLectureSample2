using UnityEngine;
using UnityEngine.UI;

public class FaceTrackingGame : MonoBehaviour
{
    float timeElapsed;
    public Text timeText;

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        timeElapsed += Time.deltaTime;
        timeText.text = timeElapsed.ToString();
    }

    private void OnCollisionExit(Collision collision)
    {
        timeElapsed = 0;
    }
}
