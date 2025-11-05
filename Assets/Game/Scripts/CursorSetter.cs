using UnityEngine;

namespace foxRestaurant
{
    public class CursorSetter : MonoBehaviour
    {
        [SerializeField] Texture2D cursor;

        void Start()
        {
            Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
        }
    }
}