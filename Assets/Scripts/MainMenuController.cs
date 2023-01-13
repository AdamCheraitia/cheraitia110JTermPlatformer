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
    //Plays a sound when you press the button
   public void Pressed()
    {
        var clip = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.Button.ToString());
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
