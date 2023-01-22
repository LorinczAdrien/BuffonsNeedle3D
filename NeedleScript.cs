using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour
{
    private LogicManagingScript logic;

    [SerializeField] private float secondsTillDestroyed = 5.0f;
    private float timeSinceSpawn = 0.0f;
    [SerializeField] private bool counted = false;

    // Start is called before the first frame update
    void Start()
    {
        this.logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagingScript>();
        this.secondsTillDestroyed = this.logic.getNeedleDeathSeconds();
    }

    // Update is called once per frame
    void Update()
    {
        this.secondsTillDestroyed = this.logic.getNeedleDeathSeconds();

        this.timeSinceSpawn += Time.deltaTime;
        if(this.timeSinceSpawn > this.secondsTillDestroyed) Destroy(gameObject);
    }

    public void setCounted(bool counted)
    {
        this.counted = counted;
    }

    public bool getCounted()
    {
        return this.counted;
    }
}
