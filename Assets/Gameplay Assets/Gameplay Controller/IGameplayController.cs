

using UnityEngine;

public interface IGameplayController
{
    public void SelectedItem(ItemController itemScript, out bool result);
    public void RemoveItem(ItemController itemScript);
    public void AddStar();
}
