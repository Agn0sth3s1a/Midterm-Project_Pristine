using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject LoseScreen;
    [SerializeField] GameObject WinScreen;

    [SerializeField] CarController1 carController;

    public static bool GameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOver = false;
    }

    public void PlayerLost()
    {
        carController.cantMoveAnymore();
        LoseScreen.SetActive(true);
    }

    IEnumerator WaitForWinScreen()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        WinScreen.SetActive(true);
    }

    public void PlayerWon()
    {
        carController.cantMoveAnymore();
        StartCoroutine(WaitForWinScreen());
    }
}
