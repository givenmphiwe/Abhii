
using UnityEngine;
using System.Collections;

public class BluePlayerOne : MonoBehaviour
{
    public static string bluePlayerOneColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            bluePlayerOneColName = col.gameObject.name;

            if (col.gameObject.name.Contains("blue house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        bluePlayerOneColName = "none";

    }
}







