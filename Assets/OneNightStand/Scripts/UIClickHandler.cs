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
        Debug.Log("Exit Clicked");
        //TODO: Quit
    }

    public void OnMenuReturnClick() {
        Debug.Log("Menu Return Clicked");
        GameObject buttonContainer = transform.Find("ButtonContainer").gameObject;
        buttonContainer.SetActive(true);

        GameObject controlsImg = transform.Find("ControlsImage").gameObject;
        controlsImg.SetActive(false);
    }
}
