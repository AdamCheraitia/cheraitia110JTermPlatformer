using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundEffectName
    {
        Shoot,
        RobotScream,
        RobotIdle,
        PlayerHit,
        Jump,
        Button,
        PickUp
    }
    public AudioClip[] SoundEffects;
    public Dictionary<string, AudioClip> SoundEffectDictionary = new Dictionary<string, AudioClip>();
    public static SoundManager SoundManagerInstance;
    //I can't even...  This some how makes a Dictionary for Sounds??? Probably the craziest thing coded in this project.
    void Start()
    {       
        if(SoundManagerInstance == null)
        {
            SoundManagerInstance = this;
            DontDestroyOnLoad(gameObject);
            var listofenums = System.Enum.GetNames(typeof(SoundEffectName));
            for (int i = 0; i < listofenums.Length; i++)
            {
                SoundEffectDictionary.Add(listofenums[i], SoundEffects[i]);
            }
        }
        else
        {
            //Destroys the game object if a Sound Manager is already present within the scene
            Destroy(gameObject);
        }
    }

    public AudioClip GetAudioClipFromDictionary(string DictionaryTerm)
    {
        AudioClip cliptoreturn;
        SoundEffectDictionary.TryGetValue(DictionaryTerm, out cliptoreturn);
        return cliptoreturn;
    }
}
