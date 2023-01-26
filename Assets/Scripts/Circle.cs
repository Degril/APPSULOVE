using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerClickHandler
{
    public bool IsMovable { get; private set; }= true;
    public void OnPointerClick(PointerEventData eventData)
    {
        IsMovable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DestroyableObject>(out var obstacle)
            && other.TryGetComponent<Square>(out _))
        {
            obstacle.DestroyWithEvent();
        }
    }
}
