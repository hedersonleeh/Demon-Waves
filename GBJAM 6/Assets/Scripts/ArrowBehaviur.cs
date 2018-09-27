using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBehaviur : MonoBehaviour {

    //posisiones

    public int canal;
    //Adinistrador de scenas
    public SceneChanger sceneChanger;

    // Use this for initialization
    void Start () {
        canal = 2;

    }

    // Update is called once per frame
    void Update () {
        if (canal > 3)

        {
            canal = 1;
        }
        if(canal < 1)
        {
            canal = 3;
        }
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            canal++;
            
       
        }
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            canal--;

        }
        if(canal == 3)
        {
            transform.position = new Vector2(transform.position.x,-0.7f);
            if(Input.GetKeyDown(KeyCode.F))
            {
                sceneChanger.ChangeSceneTo("Main Scene");
            }
        }
        if (canal == 2)
        {
            transform.position = new Vector2(transform.position.x, -0.5f);
            if (Input.GetKeyDown(KeyCode.F))
            {
                sceneChanger.ChangeSceneTo("_Scene_1");
            }
        }
        if (canal == 1)
        {
            transform.position = new Vector2(transform.position.x, -0.3f);
            if (Input.GetKeyDown(KeyCode.F))
            {
                sceneChanger.ChangeSceneTo("Credits");
            }
        }
    }
  
}
