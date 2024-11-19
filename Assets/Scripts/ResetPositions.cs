using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ResetPositions : MonoBehaviour
{
    public GameObject clearButton; // Reference to the clear button GameObject with Interactable component
    public List<GameObject> planets; // List of all the planets to reset

    private Dictionary<GameObject, Vector3> initialPositions; // Dictionary to store initial positions

    void Start()
    {
        initialPositions = new Dictionary<GameObject, Vector3>();

        // Store the initial positions of all planets
        foreach (GameObject planet in planets)
        {
            initialPositions.Add(planet, planet.transform.position);
        }

        // Add a listener to the Interactable button
        if (clearButton != null)
        {
            var interactable = clearButton.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.OnClick.AddListener(OnClearButtonClick);
            }
            else
            {
                Debug.LogError("Clear button does not have an Interactable component!");
            }
        }
        else
        {
            Debug.LogError("Clear button is not assigned!");
        }
    }

    void OnClearButtonClick()
    {
        // Reset all planets to their initial positions
        foreach (GameObject planet in planets)
        {
            if (initialPositions.ContainsKey(planet))
            {
                planet.transform.position = initialPositions[planet];
            }
        }
    }
}
