using UnityEngine;

public class TrapTrigger : MonoBehaviour
{

    public System.Action OnTriggered;
    public TrapMover trap;
    public bool trapTest = false;
    private void OnTriggerEnter(Collider other) //player walk into a trigger which then sets the trap active
    { 
        if (other.CompareTag("Player")) 
        { 
            OnTriggered?.Invoke(); 
        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        OnTriggered += trap.ActivateTrap;


    }

    // Update is called once per frame
    void Update()
    {
        if (trapTest == true)//used to test traps 
        {
            trap.ActivateTrap();
        }
    }
}
