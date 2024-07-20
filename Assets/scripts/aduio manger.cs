using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aduiomanger : MonoBehaviour
{
    public sounds[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach( sounds s in sounds )
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

        playSound("main theme");
        
    }
    public void playSound(string name)
    {
        foreach (sounds s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
            }
        }
    }
}
