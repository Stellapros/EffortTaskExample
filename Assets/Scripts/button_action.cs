using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_action : MonoBehaviour
{

    public GameObject ExpManager;
    public void ClickButtonYes()
    {

        Debug.Log("button yes is down");

        ExpManager.GetComponent<ExperimentManager>().StartTrial();

    }


    public void ClickButtonNo()
    {

        Debug.Log("button no is down");

        ExpManager.GetComponent<ExperimentManager>().SkipTrial();

    }

}
