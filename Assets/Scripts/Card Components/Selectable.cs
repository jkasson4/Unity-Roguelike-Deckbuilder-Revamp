using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    // Dependancies
    public SelectionManager selectionManager;

    // Send Selection To Selection Manager On Click
    void OnMouseDown()
    {
        selectionManager.SendSelection(gameObject);
    }
}
