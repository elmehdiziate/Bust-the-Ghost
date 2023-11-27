using UnityEngine;
using UnityEngine.UI;

public class Proba : MonoBehaviour
{
    public Button peepButton; // Reference to the Peep button
    private Gridmanager game; // Reference to the Gridmanager
    private bool showProbability = false;

    void Start()
    {
        game = FindObjectOfType<Gridmanager>(); // Get the Gridmanager instance
        peepButton.onClick.AddListener(ToggleProbability); // Add listener to Peep button
    }

    public void ToggleProbability()
    {
        showProbability = !showProbability;
        if (showProbability)
        {
            ShowProbability();
        }
        else
        {
            HideProbability();
        }
    }

    public void HideProbability()
    {
        // Logic to hide probability on each tile
        foreach (GameObject tile in game.grid)
        {
            TextMesh textMesh = tile.GetComponentInChildren<TextMesh>();
            if (textMesh != null) textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f); // Set alpha to 0
        }
    }

    public void ShowProbability()
    {
        // Logic to show probability on each tile
        foreach (GameObject tile in game.grid)
        {
            TextMesh textMesh = tile.GetComponentInChildren<TextMesh>();
            if (textMesh != null) textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f); // Set alpha to 1
        }
    }
}
