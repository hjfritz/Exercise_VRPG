using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] private GameObject _rig;
    
    [SerializeField] private bool loadPrefs;
    
    private PlayerCombatant pCombatant;

    public bool hasGloves=false;

    public int countCoins=0;
    
    public int hps=100;

    public int defeatedEnemies=0;

    public int levelprogression = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        pCombatant = Resources.FindObjectsOfTypeAll<PlayerCombatant>()[0];
        if (loadPrefs)
        {
            Load();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        hps = pCombatant.GetHP();
        PlayerPrefs.SetFloat("X", _rig.transform.position.x); 
        PlayerPrefs.SetFloat("Y", _rig.transform.position.y); 
        PlayerPrefs.SetFloat("Z", _rig.transform.position.z);
        PlayerPrefs.SetInt("Gloves", Convert.ToInt32(hasGloves));
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.SetInt("HP", hps);
        PlayerPrefs.SetInt("Enemies", defeatedEnemies);
        PlayerPrefs.SetInt("Level", levelprogression);
    }

    public void Load()
    {
        float xpos = PlayerPrefs.GetFloat("X");
        float ypos = PlayerPrefs.GetFloat("Y");
        float zpos = PlayerPrefs.GetFloat("Z");
        _rig.transform.position = new Vector3(xpos, ypos, zpos);
        countCoins = PlayerPrefs.GetInt("Coins");
        hps = PlayerPrefs.GetInt(("HP"));
        pCombatant.UpdateHP(hps);
        defeatedEnemies = PlayerPrefs.GetInt("Enemies");
        levelprogression = PlayerPrefs.GetInt("Level");
        //hasGloves= PlayerPrefs.GetInt("Gloves");
    }
    
}
