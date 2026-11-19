using UnityEngine;

public class NPCPrompt : MonoBehaviour
{
    public GameObject promptBubble;

    void Start()
    {
        if (promptBubble != null)
        {
            promptBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && promptBubble != null)
        {
            promptBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && promptBubble != null)
        {
            promptBubble.SetActive(false);
        }
    }
}