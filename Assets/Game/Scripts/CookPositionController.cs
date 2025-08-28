using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class CookPositionController : MonoBehaviour
{
    public async void HoverSlot(ItemSlot itemSlot)
    {
        transform.DOMove(Camera.main.ScreenToWorldPoint(itemSlot.transform.position + Vector3.forward * 10), 0.1f);
        await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).AsyncWaitForCompletion();
        await transform.DOScale(1f, 0.1f).AsyncWaitForCompletion();
    }
}
