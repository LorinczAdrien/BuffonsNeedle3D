using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollisionScript : MonoBehaviour
{
    public LogicManagingScript logic;

    // Start is called before the first frame update
    void Start()
    {
        // Find the refference to logic manager object
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        NeedleScript needle = other.gameObject.GetComponent<NeedleScript>();
        // If the needle wasn't counted, we count it and set it's bool value to counted
        if (!needle.getCounted())
        {
            // We increase the counter
            logic.newHit();

            // Needle has been counted
            needle.setCounted(true);
        }
    }
}
