using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDoneOnClick : MonoBehaviour
{
    // Dependancies
    public SelectionManager selectionManager;

    // Complete Selection On Click
    void OnMouseDown()
    {
        selectionManager.Done();
    }
}
