using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Windows.Speech;

public class InteractiveStar : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    public AudioSource audioSource; // Public AudioSource for attaching in the Inspector

    public AudioClip mercuryAudio;
    public AudioClip venusAudio;
    public AudioClip earthAudio;
    public AudioClip marsAudio;
    public AudioClip jupiterAudio;
    public AudioClip saturnAudio;
    public AudioClip uranusAudio;
    public AudioClip neptuneAudio;
    public AudioClip moonAudio;
    public AudioClip sunAudio; // Added Sun Audio

    public GameObject mercuryInfoBanner;
    public GameObject venusInfoBanner;
    public GameObject earthInfoBanner;
    public GameObject marsInfoBanner;
    public GameObject jupiterInfoBanner;
    public GameObject saturnInfoBanner;
    public GameObject uranusInfoBanner;
    public GameObject neptuneInfoBanner;
    public GameObject moonInfoBanner;
    public GameObject sunInfoBanner; // Added Sun Info Banner

    private Dictionary<string, (AudioClip, GameObject)> planetData;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords;

    private void Start()
    {
        // Initialize the planet data
        planetData = new Dictionary<string, (AudioClip, GameObject)>
        {
            { "mercury", (mercuryAudio, mercuryInfoBanner) },
            { "venus", (venusAudio, venusInfoBanner) },
            { "earth", (earthAudio, earthInfoBanner) },
            { "mars", (marsAudio, marsInfoBanner) },
            { "jupiter", (jupiterAudio, jupiterInfoBanner) },
            { "saturn", (saturnAudio, saturnInfoBanner) },
            { "uranus", (uranusAudio, uranusInfoBanner) },
            { "neptune", (neptuneAudio, neptuneInfoBanner) },
            { "moon", (moonAudio, moonInfoBanner) },
            { "sun", (sunAudio, sunInfoBanner) } // Added Sun Data
        };

        // Initialize speech keywords
        keywords = planetData.Keys.ToDictionary(key => key, key => new System.Action(() => RespondToQuestion(key)));

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (keywords.TryGetValue(args.text.ToLower(), out System.Action keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    private void RespondToQuestion(string planet)
    {
        if (planetData.TryGetValue(planet, out (AudioClip, GameObject) data))
        {
            // Stop any current audio and play the new one
            audioSource.Stop();
            audioSource.clip = data.Item1;
            audioSource.Play();

            // Show the relevant information banner
            foreach (var banner in planetData.Values.Select(v => v.Item2))
            {
                banner.SetActive(false);
            }
            data.Item2.SetActive(true);
        }
    }

    // Implement IMixedRealityPointerHandler methods for dragging functionality
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        transform.position = eventData.Pointer.Result.Details.Point;
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        string clickedObjectName = eventData.Pointer.Result.Details.Object.transform.name.ToLower();
        RespondToQuestion(clickedObjectName);
    }

    // Implement IMixedRealityFocusHandler methods
    public void OnFocusEnter(FocusEventData eventData)
    {
        string focusedObjectName = eventData.Pointer.Result.Details.Object.transform.name.ToLower();
        RespondToQuestion(focusedObjectName);
    }

    public void OnFocusExit(FocusEventData eventData) { }

    private void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= OnPhraseRecognized;
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
