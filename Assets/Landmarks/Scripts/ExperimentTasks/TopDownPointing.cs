using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TopDownPointing : ExperimentTask
{
    [Header("Task-specific Properties")]
    public Canvas rvdCanvas;
    public ObjectList facingList;
    public ObjectList targetList;
    public GameObject headingIcon;
    public GameObject facingIcon;
    public GameObject targetIcon;
    public float sensitivity = 50;
    
    private bool responded;
    private int trialNum;


    public override void startTask()
    {
        TASK_START();

        // LEAVE BLANK
    }


    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        if (skip)
        {
            log.log("INFO    skip task    " + name, 1);
            return;
        }
        trialNum = 1;
        InitTrial();
    }


    public override bool updateTask()
    {
        

        // rotate the response estimate
        if (Input.GetKey(KeyCode.RightArrow)) targetIcon.transform.RotateAround(headingIcon.transform.position, Vector3.back, sensitivity * Time.deltaTime);
        else if (Input.GetKey(KeyCode.LeftArrow)) targetIcon.transform.RotateAround(headingIcon.transform.position, Vector3.forward, sensitivity * Time.deltaTime);

        // Keep the image upright
        targetIcon.transform.eulerAngles = new Vector3();

        // Register the response only if it hasn't been provided already
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!responded)
            {
                responded = true;
                Debug.Log("Response submitted");

                // Log Data

                // Update tihngs
                targetList.incrementCurrent();
                trialNum++;

                // Figure out what to do next
                if (targetList.current >= targetList.objects.Count)
                {
                    targetList.current = 0;
                    targetList.objects.Clear();
                    return true;
                }
                else InitTrial();
              
            }
            else Debug.Log("You cannot submit more than one response");
        }

        return false;
    }


    public override void endTask()
    {
        TASK_END();

        // LEAVE BLANK
    }


    public override void TASK_END()
    {
        base.endTask();

        facingList.incrementCurrent();
    }

    void InitTrial()
    {
        Debug.Log("FACING " + facingList.currentObject().ToString() + "POINT TO " + targetList.currentObject().ToString());

        responded = false;

        // random start orientation
        var newz = new Vector3(0, 0, Random.Range(0, 359));
        headingIcon.transform.localEulerAngles = newz;
    }
}
