using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool stackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int maxStack { get; set; } = 1;

        [field: SerializeField]
        public string itemName;

        [field: SerializeField]
        [field: TextArea]
        public string description {get; set; }

        [field: SerializeField]
        public Sprite itemImage {get; set; }
    }
}