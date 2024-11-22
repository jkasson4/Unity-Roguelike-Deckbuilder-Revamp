using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaterial : MonoBehaviour
{
    /* --------------------------------------
     * Currently unused as no textures exist.
     * Needs to be reworked to apply to
     * image plane on card.
     * --------------------------------------
     */

    // Set Card Texture

    public void SetMaterial(string texture_name)
    {
        var texture = Resources.Load<Texture2D>("Textures/" + texture_name);
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
