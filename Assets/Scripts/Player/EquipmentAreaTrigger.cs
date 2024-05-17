using UnityEngine;

public class EquipmentAreaTrigger : MonoBehaviour
{
    [SerializeField] string PlayerTag = "Player";

    [SerializeField] GameObject equipment;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerTag)
        {
            other.GetComponent<IPlayerEquip>().Equip(equipment);
            Destroy(gameObject, 0.1f);
        }
    }
}