using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIClickHandler : MonoBehaviour
{

    public void OnGameStartClick() {
        Debug.Log("Game start");
        SceneManager.LoadScene("Bedroom", LoadSceneMode.Single);
        GlobalState.Reset();
    }

    public void OnControlsClick() {
        Debug.Log("Controls Clicked");
        GameObject buttonContainer = transform.Find("ButtonContainer").gameObject;
        buttonContainer.SetActive(false);

        GameObject controlsImg = transform.Find("ControlsImage").gameObject;
        controlsImg.SetActive(true);
    }

    public void OnExitClick() {
        Application.Quit();
    }

    public void OnMenuReturnClick() {
        GameObject buttonContainer = transform.Find("ButtonContainer").gameObject;
        buttonContainer.SetActive(true);

        GameObject controlsImg = transform.Find("ControlsImage").gameObject;
        controlsImg.SetActive(false);
    }

    public void OnExternalMenuClick() {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
}
