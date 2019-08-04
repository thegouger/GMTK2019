using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIClickHandler : MonoBehaviour
{
    public void OnGameStartClick() {
        Debug.Log("Game start");
        //TODO: Start first level
    }

    public void OnControlsClick() {
        Debug.Log("Controls Clicked");
        SceneManager.LoadScene("ControlsScreen", LoadSceneMode.Single);
    }

    public void OnExitClick() {
        Debug.Log("Exit Clicked");
        //TODO: Quit
    }

    public void OnMenuReturnClick() {
        Debug.Log("Menu Return Clicked");
        //TODO: Launch main menu
    }
}
