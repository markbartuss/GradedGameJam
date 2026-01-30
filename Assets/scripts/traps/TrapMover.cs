using UnityEngine;

public class TrapMover : MonoBehaviour
{
    // parameters for the floor thats moving
    public float riseHeight = 2f; 
    public float riseSpeed = 2f; 
    public float forwardDistance = 5f; 
    public float forwardSpeed = 3f;

    private Vector3 startPos;

    private void Awake() 
    { 
        startPos = transform.position; 
    }

    public void ActivateTrap()  // this activates the move trap 
    { 
        StartCoroutine(MoveTrap()); 
    }

    private System.Collections.IEnumerator MoveTrap()
    { 
       Vector3 targetUp = startPos + Vector3.up * riseHeight;  // goes up from below the ground
        while (Vector3.Distance(transform.position, targetUp) > 0.01f) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetUp, riseSpeed * Time.deltaTime); 
            yield return null; 
        } 

        
        Vector3 targetForward = targetUp + transform.forward * forwardDistance; //this moves the trap in a forward direction if positive / negative moves back
        while (Vector3.Distance(transform.position, targetForward) > 0.01f) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetForward, forwardSpeed * Time.deltaTime); 
            yield return null; 
        } 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
