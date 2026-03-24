using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject LoseScreen;
    [SerializeField] GameObject WinScreen;

    [SerializeField] CarController1 carController;

    [Header("Player and Enemy")]
    [SerializeField] GameObject theGuy;
    [SerializeField] GameObject thePlayer;

    [Header("His Spawn Points")]
    [SerializeField] GameObject[] HisSpawns;

    [Header("Your Spawn Points")]
    [SerializeField] GameObject[] YourSpawns;

    public static bool GameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOver = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        int chosenSpot;
        chosenSpot = UnityEngine.Random.Range(1, 4);

        Debug.Log(chosenSpot);

        if (chosenSpot == 1)
        {
            theGuy.transform.position = HisSpawns[0].transform.position;
        }
        else if (chosenSpot == 2)
        {
            theGuy.transform.position = HisSpawns[1].transform.position;
        }
        else
        {
            theGuy.transform.position = HisSpawns[2].transform.position;
        }

        chosenSpot = UnityEngine.Random.Range(1, 3);

        Debug.Log(chosenSpot);

        if (chosenSpot == 1)
        {
            thePlayer.transform.SetPositionAndRotation(YourSpawns[0].transform.position, YourSpawns[0].transform.rotation);
        }
        else if (chosenSpot == 2)
        {
            thePlayer.transform.SetPositionAndRotation(YourSpawns[1].transform.position, YourSpawns[1].transform.rotation);
        }
    }

    public void PlayerLost()
    {
        carController.cantMoveAnymore();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LoseScreen.SetActive(true);
    }

    IEnumerator WaitForWinScreen()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        WinScreen.SetActive(true);
    }

    public void PlayerWon()
    {
        carController.cantMoveAnymore();
        StartCoroutine(WaitForWinScreen());
    }
}
