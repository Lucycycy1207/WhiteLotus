using UnityEngine;
public interface IPickable
{
    void OnPicked(Transform attachTransform);
    void OnDropped();

}
