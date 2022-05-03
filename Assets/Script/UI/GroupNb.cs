using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class GroupNb : MonoBehaviour
    {
        private TextMeshProUGUI m_Text;
        private GroupSpawn _groupSpawn;

        private void Start()
        {
            _groupSpawn = GetComponentInParent<GroupSpawn>();
            m_Text = GetComponent<TextMeshProUGUI>();
            m_Text.text = $"{_groupSpawn.nbObjects}";
        }
        
        public void Increment()
        {
            _groupSpawn.nbObjects++;
            m_Text.text = $"{_groupSpawn.nbObjects}";
        }

        public void Decrement()
        {
            //ItemScale cannot go below 1
            if (_groupSpawn.nbObjects <= 1.0f) return;
            
            _groupSpawn.nbObjects--;
            m_Text.text = $"{_groupSpawn.nbObjects}";
        }
    }
}
