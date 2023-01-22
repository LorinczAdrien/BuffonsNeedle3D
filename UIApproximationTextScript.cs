using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIApproximationTextScript : MonoBehaviour
{
    // References
    [SerializeField] private Text piText;
    private LogicManagingScript logic;

    // Start is called before the first frame update
    void Start()
    {
        this.logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get current pi approximaton
        float piApproximation = this.logic.getPiApproximation();

        // Set new text
        string newPiText = new string("Pi approximation: ");
        newPiText += piApproximation.ToString();
        this.piText.text = newPiText;
    }
}
