using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] List<GameObject> closedEyes = new List<GameObject>();
    [SerializeField] List<GameObject> openedEyes = new List<GameObject>();

    public void PlayGame()
    {
        for (int i = 0; i < closedEyes.Count; i++)
        {
            closedEyes[i].SetActive(false);
        }
        for (int i = 0; i < openedEyes.Count; i++)
        {
            openedEyes[i].SetActive(true);
        }
        Invoke("LoadLevel", 2f);
    }

    public void LoadLevel()
    {
        int index = PlayerPrefs.GetInt("SavedScene");
        if(index !=0)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }

    
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    }

   
}
