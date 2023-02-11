

using UnityEngine;

public interface IGameplayController
{
    public void SelectedItem(Transform item);
    public void RemoveItem(string itemTag);
    public void AddStar();
}
