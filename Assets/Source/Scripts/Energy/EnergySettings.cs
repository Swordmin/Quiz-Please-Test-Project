using UnityEngine;

namespace EnergyRegen.Energy
{
    [CreateAssetMenu(fileName = "EnergySettings", menuName = "EnergyRegen/Energy Settings")]
    public class EnergySettings : ScriptableObject
    {
        [field: SerializeField] public int MaxEnergy { get; private set; } = 100;
        [field: SerializeField] public float RegenSeconds { get; private set; } = 5f;
    }
}
