using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    public float Health = 5f;
    public float Speed = 5f;
    public float Damage = 1f;
    public float attackRange = 1f;

}
