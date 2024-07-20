using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endtriger : MonoBehaviour
{
    public playermanger playermanger;
    private void OnTriggerEnter(Collider other)
    {
        playermanger.completeLevel();
        Time.timeScale = 0;
    }
}
