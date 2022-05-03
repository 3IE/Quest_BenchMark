using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class SpawnButton : MonoBehaviour, IPointerDownHandler
    {
        private GroupSpawn _groupSpawn;

        private void Start()
            => _groupSpawn = GetComponentInParent<GroupSpawn>();

        public void OnPointerDown(PointerEventData eventData)
            => _groupSpawn.SpawnGroup();
    }
}
