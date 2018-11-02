using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Vector2 uvOffset = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        uvOffset += GameControl.instance.move * Time.deltaTime;
        GetComponent<Renderer>().materials[0].SetTextureOffset("_MainTex", uvOffset);
    }
}
