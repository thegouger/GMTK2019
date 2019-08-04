using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIClickHandler : MonoBehaviour
{

    private GameObject buttonContainer;
    private GameObject controlsImg;

    void Start() {
        buttonContainer = transform.Find("ButtonContainer").gameObject;
        controlsImg = transform.Find("ControlsImage").gameObject;
    }

    public void OnGameStartClick() {
        Debug.Log("Game start");
        SceneManager.LoadScene("Bedroom", LoadSceneMode.Single);
    }

    public void OnControlsClick() {
        Debug.Log("Controls Clicked");
        buttonContainer.SetActive(false);
        controlsImg.SetActive(true);
    }

    public void OnExitClick() {
        Debug.Log("Exit Clicked");
        //TODO: Quit
    }

    public void OnMenuReturnClick() {
        Debug.Log("Menu Return Clicked");
        buttonContainer.SetActive(true);
        controlsImg.SetActive(false);
    }
}
