using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string IS_MUTED = "isMuted";
    [SerializeField] AudioSource m_Source;
    void Start()
    {
        if (PlayerPrefs.HasKey(IS_MUTED))
        {
            if (PlayerPrefs.GetInt(IS_MUTED) == 0)
            {
                m_Source.enabled = true;
            }
            else
            {
                m_Source.enabled = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt(IS_MUTED, 0);
        }
    }
    public void ClickAudioButtton()
    {
        if(PlayerPrefs.GetInt(IS_MUTED)==0)
        {
            MuteAudio();
        }
        else
        {
            UnMuteAudio();
        }
    }
    public void MuteAudio()
    {
        m_Source.enabled=false;
        PlayerPrefs.SetInt(IS_MUTED, 1);
    }
    public void UnMuteAudio()
    {
        m_Source.enabled = true;
        PlayerPrefs.SetInt(IS_MUTED, 0);
    }
}
