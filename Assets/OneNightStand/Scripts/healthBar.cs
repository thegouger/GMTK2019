using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    private float maxHealth = 100;
    private float curHealth = 100;
   
    // Use this for initialization
    void Start () {
        var enemyController = gameObject.GetComponent<EnemyController>();
        maxHealth = enemyController.health;
    }
   
    // Update is called once per frame
    void Update () {
        var enemyController = gameObject.GetComponent<EnemyController>();
        curHealth = enemyController.health;
    }
   
    void OnGUI() {
        if(curHealth < maxHealth && curHealth > 0)
        {
            Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
            var oldColour = GUI.color;

            GUI.color = Color.red;
            GUI.Box(new Rect(targetPos.x, Screen.height-targetPos.y-Screen.height*0.1f, 60, 20), Mathf.Round(curHealth) + "/" + Mathf.Round(maxHealth));

            GUI.color = oldColour;
        }
    }
}
