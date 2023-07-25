using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public int IsSoundOn;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;
    
    Image SoundIcon;
    // Start is called before the first frame update
    void Start()
    {
        IsSoundOn = PlayerPrefs.GetInt("IsSoundOn", 1);
        SoundIcon = GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<Image>();
        SoundIcon.sprite = IsSoundOn == 1 ? SoundOnImage : SoundOffImage;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ToggleSound()
    {
        if(SoundIcon == null)
        {
            SoundIcon = GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<Image>();
        }
        IsSoundOn = IsSoundOn == 1 ? 0 : 1;
        PlayerPrefs.SetInt("IsSoundOn", IsSoundOn);
        SoundIcon.sprite = IsSoundOn == 1 ? SoundOnImage : SoundOffImage;
    }
}
