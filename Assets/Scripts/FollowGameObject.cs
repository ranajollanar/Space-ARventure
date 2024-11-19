using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    // Public field to assign the GameObject to follow in the Inspector
    public GameObject gameObjectToFollow;

    // Update is called once per frame
    void Update()
    {
        // Check if the gameObjectToFollow is assigned
        if (gameObjectToFollow != null)
        {
            // Update the position of this GameObject to match the position of the gameObjectToFollow
            transform.position = gameObjectToFollow.transform.position;
        }
        else
        {
            Debug.LogWarning("gameObjectToFollow is not assigned.");
        }
    }
}
