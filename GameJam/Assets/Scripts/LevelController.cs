using System.Collections.Generic;
using UnityEngine;

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
        if(!AnyBanditAlive() || OutOfAmmo())
            Debug.Log("Spreman za sledeci nivo");
    }
}
