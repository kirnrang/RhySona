using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject player;
    private PlayerControl check_gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        menupanel.SetActive(false);
        check_gameEnd = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(check_gameEnd.gamecheck() == true)
        {
            menupanel.SetActive(true);
        }
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
