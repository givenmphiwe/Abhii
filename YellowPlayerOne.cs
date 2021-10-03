using System.Collections;
using UnityEngine;

public class YellowPlayerOne : MonoBehaviour
{
    public static string yellowPlayerOneColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            yellowPlayerOneColName = col.gameObject.name;

            if (col.gameObject.name.Contains("yellow house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        yellowPlayerOneColName = "none";
    }
}
