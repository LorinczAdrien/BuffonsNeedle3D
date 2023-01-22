using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManagingScript : MonoBehaviour
{
    // Refference to spawner, for spawned needles
    [SerializeField] private NeedleSpawningScript needleSpawningScript;

    // Control variables
    [SerializeField] private int lineCount = 10;
    [SerializeField] private float needleSpawnRateInSeconds = 100.0f;
    [SerializeField] private int needleMax = 100000;
    [SerializeField] private float needleSizePercent = 90.0f;
    [SerializeField] private float spawnCutoffMultiplier = 0.5f;
    [SerializeField] private float needleDeathSeconds = 5.0f;

    // Pi approximation variables
    [SerializeField] private int spawnedNeedles = 0, hits = 0;
    [SerializeField] private float piApproximation = 0.0f;
    private float lineDistance = 2.0f, needleLength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.lineDistance = 20.0f / (float)this.lineCount;
        this.needleLength = this.getNeedleLength();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the number of dropped needles, untill now
        this.spawnedNeedles = needleSpawningScript.getNeedlesSpawned();

        // New approximation
        if (this.hits > 0)
        {
            this.piApproximation = (2 * this.needleLength * (float) this.spawnedNeedles) / (this.lineDistance * (float) this.hits);
        }
    }

    // Increases the hit counter by one

    private float getNeedleLength()
    {
        return this.lineDistance;
    }

    public void newHit()
    {
        this.hits++;
    }

    public int getNeedleMax()
    {
        return this.needleMax;
    }

    public float getNeedleDeathSeconds()
    {
        return this.needleDeathSeconds;
    }

    public float getneedleSpawnRateInSeconds()
    {
        return this.needleSpawnRateInSeconds;
    }

    public float getNeedleSizePercent()
    {
        return this.needleSizePercent;
    }

    public float getCutoffMultiplier()
    {
        return this.spawnCutoffMultiplier;
    }

    public float getLineDistance()
    {
        return this.lineDistance;
    }

    public float getPiApproximation()
    {
        return this.piApproximation;
    }
}
