using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private GameObject[] bandits;

    public static LevelController Instance;

    #region MonoBehaviour Events
    private void Awake()
    {
        Instance = this;

        bandits = GameObject.FindGameObjectsWithTag("Enemy");
    }
    #endregion

    public bool AnyBanditAlive()
    {
        foreach (GameObject bandit in bandits)
        {
            if (bandit.activeInHierarchy)
                return true;
        }

        return false;
    }

    public bool OutOfAmmo() //Out of ammo and bullets not in scene
    {
        if (CowboyController.Instance.Ammo == 0)
        {
            List<GameObject> bullets = CowboyController.Instance.Bullets;

            foreach (GameObject bullet in bullets)
            {
                if (bullet.activeInHierarchy)
                    return false;
            }
            return true;
        }

        return false;
    }

    public void CheckForNextLevel()
    {
        if(!AnyBanditAlive())
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            Fader.LevelLoad(nextLevel);
        }
        else if(OutOfAmmo())
            Fader.LevelLoad(SceneManager.GetActiveScene().buildIndex);
    }
}
