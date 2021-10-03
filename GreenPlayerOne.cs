using System.Collections;
using UnityEngine;

public class GreenPlayerOne : MonoBehaviour
{
    public static string greenPlayerOneColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            greenPlayerOneColName = col.gameObject.name;

            if (col.gameObject.name.Contains("green house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        greenPlayerOneColName = "none";
    }
}
