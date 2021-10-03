using System.Collections;
using UnityEngine;

public class YellowPlayerThree : MonoBehaviour
{
    public static string yellowPlayerThreeColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            yellowPlayerThreeColName = col.gameObject.name;

            if (col.gameObject.name.Contains("yellow house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        yellowPlayerThreeColName = "none";
    }
}
