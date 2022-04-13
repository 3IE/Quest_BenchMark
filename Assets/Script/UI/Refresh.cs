using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class Refresh : MonoBehaviour, IPointerDownHandler
    {
        private RefreshList _refreshList;
        private void Start()
            => _refreshList = GetComponentInParent<RefreshList>();

        public void OnPointerDown(PointerEventData eventData)
            => _refreshList.Refresh();
    }
}
