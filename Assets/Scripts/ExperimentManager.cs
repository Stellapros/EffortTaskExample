using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



class Trial
{
    /// <summary>
    /// Create class Trial that holds the effort level for each trial
    /// TO DO: Add other trial variables here: Player reset position, Target reset position, Reward value 
    /// </summary>
    /// varEV is the variable that holds the effort level (1,2,3) for each trial
    public float varEV = -99;
    public Vector3 varPosPlayer = new Vector3(0, 0, 0);

    public Trial(float varEV_, Vector3 varPosPlayer_)
    {
        varEV = varEV_; 
        varPosPlayer = varPosPlayer_;
        //varPosReward = varPosReward_'
        // Add here the variables that define each trial 
    }
}



public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}



public class ExperimentManager : MonoBehaviour
{
    private float startTime;
    private int trialNum = 0;
    public bool trialRunning = false;

    public GameObject goAgent;
    public TMP_Text EVText;
    public int totalKeyPresses;
    public int pressesRequired;
    public GameObject UICanvas;
    private float trialDuration = 5f;
    //List<float> lowEffort, mediumEffort, highEffort;
    //List<(float, float, float)>listLevels=new List<(float, float, float)>();
    private float lowEffort = 0.3f;
    private float mediumEffort = 0.5f;
    private float highEffort = 0.9f;


    List<Trial> lstEVs;

    void Start()
    /// <summary>
    /// Start is called before the first frame update of the scene.
    /// It is called once, when transitioning from the callibration to the game scene.
    /// </summary>

    {
       
        
        
        //TRIAL GENERATOR ///////////////////////////////////////////
        //TO DO: Create a rountine that creates a trial array that counterbalances effort level, character position, target position and  reward
        //At the moment it creates a list of trials with shuffled effort values
        lstEVs = new List<Trial>(); //lstEVs = list of trials
        //The nested for loop bellow ensures that the trial list has equal numbers of each effort value type
        //TO DO: Create a new routine for trial counterbalancing
        for (int i2 = 0; i2 < 4; i2++)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                //lstEVs is the list of trial objects
                //The new trial generation routine has to add new trial objects to this list
                lstEVs.Add(new Trial(ii+1, new Vector3(0,5,0)));
            }
        }
        //TO DO: Rethink shuffle for blocks list 
        lstEVs.Shuffle();
        

        ////// Print the trial list 
        //int totalTrials = lstEVs.Count;
        //Debug.Log("The total number of trials is:" + totalTrials);
        //Debug.Log("List of trial effort values");
        //for (int i = 0; i < totalTrials; i++)
        //{
        //    //Debug.Log("The effort value of trial" + i + "is:" + lstEVs[i].varEV);
        //}

        //Update the text field on the choice panel of each trial to show the effort level of the next trial
        //so that participants can decide wether to proceed or not
        //EVText.text = lstEVs[trialNum].varEV.ToString();

        //At the start of the first trial:
        //Get information about the effort level of the next trial according to the trial list 
        float effortLevel = lstEVs[trialNum].varEV;
        //TO DO: Change level indications from text (1,2,3) to images (square, circle, triangle)
        //Randomize the correspondance between effort level and shape for each participant
        EVText.text = effortLevel.ToString();

        //Get the number of totalKeyPresses per second achieved during callibration
        totalKeyPresses = PlayerPrefs.GetInt("totalKeyPresses");
        Debug.Log("ExperimentManager::Start()::" + totalKeyPresses);
        //Compute the steps required for movement according to the effort value assigned to the bext trial
        if (effortLevel == 1)
        {
            pressesRequired = (int)(lowEffort * totalKeyPresses);
        }
        else if (effortLevel == 2)
        {
            pressesRequired = (int)(mediumEffort * totalKeyPresses);
        }
        else if (effortLevel == 3)
        {
            pressesRequired = (int)(highEffort * totalKeyPresses);
        }
        Debug.Log("Effort level of next : " + effortLevel);
        Debug.Log("Presses Required: " + pressesRequired);
        goAgent.GetComponent<PlayerController>().moveStepTreshold = pressesRequired;

    }




    void Update()
    {
       
        /// If the trial is running, meaning the participant has clicked 'Yes' and the choice panel has been hidden
        if (trialRunning) {
            /// At the moment the trial ends with a time out
            /// TO DO: End trial either when the player collides with the target object or due to time-out
            if (Time.time - startTime > trialDuration)
            {
                Debug.Log("Time Out Trial");
                //At the end of each trial:
                //Get information about the effort level of the next trial according to the trial list 
                float effortLevel = lstEVs[trialNum].varEV;
                

                //Add to the data file, the current trial number, its effort level, participants choice (E/N). Here is always Yes because this
                //line is run at the end of a trial (meaning participant chose Yes) 
                LogManager.instance.WriteTimeStampedEntry(trialNum.ToString() + ";" + effortLevel.ToString() + ";Y");


                //Increase the trial count
                trialNum++;


                float next_effortLevel = lstEVs[trialNum].varEV;

                // define effort levels
                //listLevels.Add((0.3f, 0.5f, 0.9f));
                //Compute the steps required for movement according to the effort value assigned to the bext trial
                if (next_effortLevel == 1)
                {
                    pressesRequired = (int)(lowEffort * totalKeyPresses);
                }
                else if (next_effortLevel == 2)
                {
                    pressesRequired = (int)(mediumEffort * totalKeyPresses);
                }
                else if (next_effortLevel == 3)
                {
                    pressesRequired = (int)(highEffort * totalKeyPresses);
                }
                Debug.Log("Next Effort level: " + next_effortLevel);
                Debug.Log("Presses Required: " + pressesRequired);
                //Update the PlayerControler so it has the information about the presses required to move in the next trial
                goAgent.GetComponent<PlayerController>().moveStepTreshold = pressesRequired;

                startTime = Time.time;
                //Reset position
                //To Do: read the reset position for each trial from the trial list 
                goAgent.GetComponent<PlayerController>().ResetPosition();

                //Set trialRunning to False and Show the choice pannel for the next trial
                trialRunning = false;
                UICanvas.GetComponent<Canvas>().enabled = true;


            }
        }

    }

    public void StartTrial() {


        UICanvas.GetComponent<Canvas>().enabled = false;
        startTime = Time.time;
        trialRunning = true;

    }

    public void SkipTrial()
    {

        float effortLevel = lstEVs[trialNum].varEV;

        //Add to the data file, the current trial number, its effort level, participants choice (E/N). Here is always No because this
        //line is run when participant chooses No to skip the trial 
        LogManager.instance.WriteTimeStampedEntry(trialNum.ToString() + ";" + lstEVs[trialNum].varEV.ToString() + ";N");



        //Increase trial number
        trialNum++;

        //Needs a wait pannel before showing the next trial

        float next_effortLevel = lstEVs[trialNum].varEV;
        //Compute the steps required for movement according to the effort value assigned to the bext trial
        if (next_effortLevel == 1)
        {
            pressesRequired = (int)(lowEffort * totalKeyPresses);
        }
        else if (next_effortLevel == 2)
        {
            pressesRequired = (int)(mediumEffort * totalKeyPresses);
        }
        else if (next_effortLevel == 3)
        {
            pressesRequired = (int)(highEffort * totalKeyPresses);
        }
        Debug.Log("Next Effort level: " + next_effortLevel);
        Debug.Log("Presses Required: " + pressesRequired);
        //Update the PlayerControler so it has the information about the presses required to move in the next trial
        goAgent.GetComponent<PlayerController>().moveStepTreshold = pressesRequired;

        //Show on the choice pannel the trial level for the next trial
        EVText.text = lstEVs[trialNum].varEV.ToString();
    }

}
