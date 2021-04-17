using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using USE_States;

public class ControlLevel_Trial : ControlLevel
{

    //scene elements
    public GameObject trialStim;
    public GameObject goCue;
    public GameObject fb;

    //trial variables
    [System.NonSerialized]
    public int trialCount = 1;
    [System.NonSerialized]
    public int response = -1;


    public override void DefineControlLevel()
    {
        State stimOn = new State("StimPres");
        State collectResponse = new State("Response");
        State feedback = new State("Feedback");
        State iti = new State("ITI");
        AddActiveStates(new List<State> { stimOn, collectResponse, feedback, iti });

        //Define stimOn State
        stimOn.AddStateIntialiaztionMethod(() =>
        {
            trialStim.SetActive(true);
            response = -1;
            Debug.Log("Starting trial " + trialCount);
        });
        stim0n.AddTimer(1f, collectResponse);

        //Define collectResponse State
        collectResponse.AddStateInitializationMethod(() => goCue.SetActive(true));
        collectResponse.AddStateUpdatemethod(() =>
        {
            Ray ray = Camera.main.ScreenPointToRay(InputBroker.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == trialStim)
                    reponse = 1;
                else
                    response = 0;
            }
            else
            {
                response = 2;
            }
        });

        collecetResponse.AddTimer(5f, feedback);
        collectResponse.SpecifyStateTermination(() => response > -1, feedback);
        collectResponse.AddStateDefaultTerminationMethod(() => goCue.SetActive(false));

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

 

}