using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] Button MuteButton;
    [SerializeField] Sprite MuteSprite;
    [SerializeField] Sprite UnMuteSprite;

    bool isMute;

    private void Awake()
    {
        isMute = false;
        Debug.Log(isMute);
    }

    public void Mute()
    {
        isMute = !isMute;

        Debug.Log(isMute);

        if (isMute == true)
            MuteButton.GetComponent<Image>().sprite = MuteSprite;
        else
            MuteButton.GetComponent<Image>().sprite = UnMuteSprite;

        AudioListener.volume = isMute ? 0 : 1;
    }
}
