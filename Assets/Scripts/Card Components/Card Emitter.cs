using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEmitter : MonoBehaviour
{
    // Dependancies
    public ParticleSystem selection_source;
    public ParticleSystem selection_selected;
    public ParticleSystem actions_remaining;

    // Play And Stop Selection Source Particles

    public void PlaySelectionSource()
    {
        selection_source.Play();
    }

    public void StopSelectionSource()
    {
        selection_source.Stop();
    }

    // Play And Stop Selection Selected Particles

    public void PlaySelectionSelected()
    {
        selection_selected.Play();
    }

    public void StopSelectionSelected()
    {
        selection_selected.Stop();
    }

    // Play And Stop Actions Remaining Particles

    public void PlayActionsRemaining()
    {
        actions_remaining.Play();
    }

    public void StopActionsRemaining()
    {
        actions_remaining.Stop();
    }
}
