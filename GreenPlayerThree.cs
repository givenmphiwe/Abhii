using System.Collections;
using UnityEngine;

public class GreenPlayerThree : MonoBehaviour
{
    public static string greenPlayerThreeColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            greenPlayerThreeColName = col.gameObject.name;

            if (col.gameObject.name.Contains("green house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        greenPlayerThreeColName = "none";
    }
}
