using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<WeaponData> weapons = new List<WeaponData>();
    public List<Artifact> artifacts = new List<Artifact>();
    public Dictionary<string, int> materials = new Dictionary<string, int>();

    public void AddMaterial(string id, int count)
    {
        if (!materials.ContainsKey(id)) materials[id] = 0;
        materials[id] += count;
    }
    public bool ConsumeMaterial(string id, int count)
    {
        if (!materials.ContainsKey(id) || materials[id] < count) return false;
        materials[id] -= count; return true;
    }
}
