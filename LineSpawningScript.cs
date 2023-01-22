using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawningScript : MonoBehaviour
{
    // Reference to the plane on which the lines sit
    [SerializeField] private GameObject line, collisionPlane;
    // Reference to logic
    private LogicManagingScript logic;

    // For generating the lines
    [SerializeField] private float lineDistance = 2.0f;
    private float planeLength = 0.0f;

    // A list containing all the lines
    private List<GameObject> lines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Get distance between lines
        this.logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagingScript>();
        this.lineDistance = this.logic.getLineDistance();

        // We need to generate all the lines given the spacing

        // Get the bounds of the plane (So we know in what interval the lines should be)
        Renderer renderer = this.collisionPlane.GetComponent<Renderer>();
        Vector3 boundsMin = renderer.bounds.min;
        Vector3 boundsMax = renderer.bounds.max;

        // We only care about the difference in the 'z' coordinates -> The length of the plane
        this.planeLength = (int)(Math.Abs(boundsMax.z - boundsMin.z));

        // How many lines we can fit, with the given distance (We leave half line distance on each size)
        int lineCount = (int) this.planeLength / (int) this.lineDistance;

        // The starting position at which we will generate them
        Vector3 spawnPos = new Vector3(boundsMin.x + this.planeLength / 2, boundsMax.y, boundsMin.z + this.lineDistance / 2);

        // Instanstiate and position the lines
        for(int i = 0; i < lineCount; ++i)
        {
            this.lines.Add(Instantiate(line, spawnPos, line.transform.rotation));
            spawnPos.z += this.lineDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.lineDistance != this.logic.getLineDistance())
        {
            this.lineDistance = this.logic.getLineDistance();
            Debug.Log("New line distance: " + this.lineDistance);
            foreach(var objects in this.lines)
            {
                Destroy(objects);
            }
            this.lines.Clear();

            // We need to generate all the lines given the spacing

            // Get the bounds of the plane (So we know in what interval the lines should be)
            Renderer renderer = this.collisionPlane.GetComponent<Renderer>();
            Vector3 boundsMin = renderer.bounds.min;
            Vector3 boundsMax = renderer.bounds.max;

            // We only care about the difference in the 'z' coordinates -> The length of the plane
            this.planeLength = (int)(Math.Abs(boundsMax.z - boundsMin.z));

            // How many lines we can fit, with the given distance (We leave half line distance on each size)
            int lineCount = (int) (this.planeLength / this.lineDistance);

            // The starting position at which we will generate them
            Vector3 spawnPos = new Vector3(boundsMin.x + this.planeLength / 2, boundsMax.y, boundsMin.z + this.lineDistance / 2);

            // Instanstiate and position the lines
            for (int i = 0; i < lineCount; ++i)
            {
                this.lines.Add(Instantiate(line, spawnPos, line.transform.rotation));
                spawnPos.z += this.lineDistance;
            }
        }
    }
}
