using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToImageTracking()
    {
        SceneManager.LoadScene("ImageTracking");
    }


    public void GoToPlaneDetection()
    {
        SceneManager.LoadScene("PlaneDetection");
    }


    public void GoToQRReader()
    {
        SceneManager.LoadScene("QRReader");
    }


    public void ExitApplication()
    {

    }
}