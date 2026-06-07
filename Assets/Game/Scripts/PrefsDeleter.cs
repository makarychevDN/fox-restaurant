using UnityEngine;

public class PrefsDeleter : MonoBehaviour
{
    [ContextMenu("Delete Player Prefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Player Prefs Deleted");
    }
}
