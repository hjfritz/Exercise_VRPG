using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] private GameObject _rig;

    public bool hasGloves=false;

    public int countCoins=0;
    
    public int hps=100;

    public int defeatedEnemies=0;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if (PlayerPrefs.GetFloat("X") != null)
        {
            Load();
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("X", _rig.transform.position.x); 
        PlayerPrefs.SetFloat("Y", _rig.transform.position.y); 
        PlayerPrefs.SetFloat("Z", _rig.transform.position.z);
        PlayerPrefs.SetInt("Gloves", Convert.ToInt32(hasGloves));
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.SetInt("HP", hps);
        PlayerPrefs.SetInt("Enemies", defeatedEnemies);
    }

    public void Load()
    {
        float xpos = PlayerPrefs.GetFloat("X");
        float ypos = PlayerPrefs.GetFloat("Y");
        float zpos = PlayerPrefs.GetFloat("Z");
        _rig.transform.position = new Vector3(xpos, ypos, zpos);
        countCoins = PlayerPrefs.GetInt("Coins");
        hps = PlayerPrefs.GetInt(("HP"));
        defeatedEnemies = PlayerPrefs.GetInt("Enemies");
        //hasGloves= PlayerPrefs.GetInt("Gloves");
    }
    
}
