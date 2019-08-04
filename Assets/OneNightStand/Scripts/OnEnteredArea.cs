using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnteredArea : MonoBehaviour
{
    // Start is called before the first frame update
    public string newLevel = "Basement";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        Application.LoadLevel(newLevel);
    }
}
