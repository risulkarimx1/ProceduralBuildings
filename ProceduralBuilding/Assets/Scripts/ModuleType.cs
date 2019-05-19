using UnityEngine;

[CreateAssetMenu(fileName = "ModuleType", menuName = "AAI/ModularBuilding", order = 1)]
public class ModuleType : ScriptableObject
{
    public enum Type
    {
        Window,Corner,Entry
    }
}
