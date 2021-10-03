using System.Collections;
using UnityEngine;

public class BluePlayerThree : MonoBehaviour
{
    public static string bluePlayerThreeColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            bluePlayerThreeColName = col.gameObject.name;

            if (col.gameObject.name.Contains("blue house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        bluePlayerThreeColName = "none";
    }

}
