using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public const string SOUND_CONTROLLER_PREFAB_PATH = "Prefabs/UI/Sound Control Canvas";

    public static Sound Instance;

    [SerializeField] private Button MuteButton;
    [SerializeField] private Sprite MuteSprite;
    [SerializeField] private Sprite UnMuteSprite;

    bool isMute;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        Instance = Instantiate(Resources.Load<GameObject>(SOUND_CONTROLLER_PREFAB_PATH)).GetComponent<Sound>();
    }

    private void Start()
    {
        isMute = false;
        DontDestroyOnLoad(gameObject);
    }

    public void Mute()
    {
        isMute = !isMute;
        
        if (isMute == true)
            MuteButton.GetComponent<Image>().sprite = MuteSprite;
        else
            MuteButton.GetComponent<Image>().sprite = UnMuteSprite;

        AudioListener.volume = isMute ? 0 : 1;
    }
}
