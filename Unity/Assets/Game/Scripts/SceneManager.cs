using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneNames CurrentScene
    {
        get;
        set;
    }

    static SceneManager()
    {
        SceneManager.CurrentScene = SceneNames.StreetClub;
    }
}
