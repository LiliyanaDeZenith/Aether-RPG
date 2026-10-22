using UnityEngine;

public class ElementReactionManager : Singleton<ElementReactionManager>
{
    static bool Pair(ElementType a, ElementType d, ElementType x, ElementType y)
    { return (a == x && d == y) || (a == y && d == x); }

    public float CalculateReactionDamage(ElementType a, ElementType d, float baseDamage)
    {
        float m = 1f; string reaction = "";
        if (Pair(a, d, ElementType.Hydro, ElementType.Pyro)) { m = 2f; reaction = "Vaporize!"; }
        else if (Pair(a, d, ElementType.Cryo, ElementType.Pyro)) { m = 2f; reaction = "Melt!"; }
        else if (Pair(a, d, ElementType.Pyro, ElementType.Electro)) { m = 1.5f; reaction = "Overload!"; }
        else if (Pair(a, d, ElementType.Cryo, ElementType.Hydro)) { m = 1f; reaction = "Frozen!"; }
        else if (a == ElementType.Anemo && d != ElementType.Anemo) { m = 1.2f; reaction = "Swirl!"; }
        else if (a == ElementType.Dendro && d == ElementType.Pyro) { m = 1.5f; reaction = "Burning!"; }
        else if (Pair(a, d, ElementType.Dendro, ElementType.Hydro)) { m = 1.5f; reaction = "Bloom!"; }
        else if (a == ElementType.Geo && d != ElementType.Geo && d != ElementType.Anemo) { m = 0.5f; reaction = "Crystallize! +Perisai"; }
        if (reaction != "") Debug.Log(reaction + " DMG x" + m);
        return baseDamage * m;
    }
}
