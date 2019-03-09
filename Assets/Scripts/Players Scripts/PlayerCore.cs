using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore self;
    public static PlayerStats playerData;


    void Awake()
    {
        if(self != this) {
            self = this;
            DontDestroyOnLoad(gameObject);
            playerData = new PlayerStats();
        } else {
            Destroy(gameObject);
        }
    }

}
