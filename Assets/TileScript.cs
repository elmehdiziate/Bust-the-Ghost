using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Gridmanager gridManager;

    public void Setup(Gridmanager manager)
    {
        gridManager = manager;
    }

    void OnMouseDown()
    {
        Debug.Log("Cell Clicked: " + transform.position);
        gridManager.OnCellClicked(transform.position);
    }
}
