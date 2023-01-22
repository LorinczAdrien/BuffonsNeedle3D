using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeedleSpawningScript : MonoBehaviour
{
    // References
    public GameObject needle;
    private LogicManagingScript logic;

    [SerializeField] private int needlesSpawned = 0, needlesMax = 0;
    [SerializeField] private float spawnRateInSeconds = 1.0f;
    private float spawnTimer = 0.0f, spawnRate = 0.0f, needleSizeMultiplyer = 0.0f, needleSizePercent = 90.0f;
    private Vector3 boundsMin = Vector3.zero, boundsMax = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Get spawn rate and max needles from logic object
        this.logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagingScript>();
        this.needlesMax = this.logic.getNeedleMax();
        this.needleSizeMultiplyer = this.logic.getLineDistance();

        // Get the bounds of the object
        Renderer boundingRenderer = GetComponent<Renderer>();
        this.boundsMin = boundingRenderer.bounds.min;
        this.boundsMax = boundingRenderer.bounds.max;

        // Calculate the spawn rate from the seconds
        this.spawnRateInSeconds = this.logic.getneedleSpawnRateInSeconds();
        this.spawnRate = 1 / spawnRateInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Update needle size percent
        this.needleSizePercent = this.logic.getNeedleSizePercent();
        this.needleSizeMultiplyer = this.logic.getLineDistance();

        // Calculate the spawn rate from the seconds
        this.spawnRateInSeconds = this.logic.getneedleSpawnRateInSeconds();
        this.spawnRate = 1 / spawnRateInSeconds;

        // If we have spawned enough object, we destroy the spawner
        if (this.needlesSpawned >= this.needlesMax)
        {
            Destroy(gameObject);
        }

        // Check if we can spawn a needle
        if(this.spawnTimer < this.spawnRate)
        {
            this.spawnTimer += Time.deltaTime;
        }
        else
        {
            // Instantiate a new needle object

            // With random position inside the bounding box
            Vector3 randomPoz = Vector3.zero;
            float cutoffMultiplier = this.logic.getCutoffMultiplier();
            randomPoz.x = Random.Range(this.boundsMin.x + this.needleSizeMultiplyer * cutoffMultiplier, this.boundsMax.x - this.needleSizeMultiplyer * cutoffMultiplier);
            randomPoz.y = Random.Range(this.boundsMin.y, this.boundsMax.y);
            randomPoz.z = Random.Range(this.boundsMin.z + this.needleSizeMultiplyer * cutoffMultiplier, this.boundsMax.z - this.needleSizeMultiplyer * cutoffMultiplier);

            GameObject newNeedle = Instantiate(this.needle, randomPoz, Random.rotationUniform);    // With random position (In the bounds) and random rotation
            newNeedle.transform.localScale *= this.needleSizeMultiplyer * this.needleSizePercent / 100;                // x % of the distance between the lines

            // Change color of new needle through renderer
            Renderer rend = newNeedle.GetComponent<Renderer>();
            rend.material.color = Random.ColorHSV();        // Random color

            // Reset timer and increase count
            this.spawnTimer = 0.0f; this.needlesSpawned += 1;
        }
    }

    public int getNeedlesSpawned()
    {
        return needlesSpawned;
    }
}
