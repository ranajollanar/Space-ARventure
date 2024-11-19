using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlay : MonoBehaviour
{
    public AudioClip RedSound;
    private AudioSource audioSource;
    public GameObject panel;
    public GameObject planetObject; // Added public GameObject for the planet
    public GameObject movingPlanet; // Added public GameObject for the moving planet

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (planetObject != null)
        {
            planetObject.SetActive(false); // Ensure the planet object is initially inactive
        }

        if (movingPlanet != null)
        {
            var meshRenderer = movingPlanet.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true; // Ensure the MeshRenderer is initially active
            }
        }
    }

    public void PlaySound()
    {
        panel.SetActive(true);
        if (planetObject != null)
        {
            planetObject.SetActive(true); // Activate the planet object
        }

        if (movingPlanet != null)
        {
            var meshRenderer = movingPlanet.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false; // Deactivate the MeshRenderer
            }
        }

        audioSource.clip = RedSound;
        audioSource.Play();
    }

    public void StopSound()
    {
        panel.SetActive(false);
        if (planetObject != null)
        {
            planetObject.SetActive(false); // Deactivate the planet object
        }

        if (movingPlanet != null)
        {
            var meshRenderer = movingPlanet.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true; // Activate the MeshRenderer
            }
        }

        audioSource.Stop();
    }
}
