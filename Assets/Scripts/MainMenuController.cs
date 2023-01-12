using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public void LoadSceneWithNumber(int sceneToLoad)
   {
        SceneManager.LoadScene(sceneToLoad);
   }

   public void QuitGame()
   {
        Application.Quit();
   }
}
