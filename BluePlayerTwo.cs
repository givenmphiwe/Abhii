using System.Collections;
using UnityEngine;

public class BluePlayerTwo : MonoBehaviour
{

    public static string bluePlayerTwoColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            bluePlayerTwoColName = col.gameObject.name;

            if (col.gameObject.name.Contains("blue house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bluePlayerTwoColName = "none";

    }
}
