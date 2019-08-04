using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalState.generators[0])
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
            transform.eulerAngles = new Vector3(0f, 0f, -90f);
        
    }
}
