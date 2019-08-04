using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldExit : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite openDoors;
    public Sprite closedDoors;
    public string endGameScene;
    public int numGenerators = 4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allGeneratorsOn = true;
        int count = 0;
        for(int i = 0; i < numGenerators; i++)
        {
            allGeneratorsOn &= GlobalState.generators[i];
        }

        if(allGeneratorsOn)
        {
            Debug.Log("Win");
            gameObject.GetComponent<SpriteRenderer>().sprite = openDoors;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Win");
        // Game over - we won!
        if(col.gameObject.tag == "Player")
        {
            Application.LoadLevel(endGameScene);
        }
        
    }
}
