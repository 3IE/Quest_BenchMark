using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class DrawCallsNb : MonoBehaviour
    {
        private int nbObjs;
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            InvokeRepeating(nameof(DrawNb), 1, 1);
        }

        private void DrawNb()
        {
            //smoothDeltaTime, deltaTime, fixedDeltaTime
            //nbObjs = UnityStats.drawCalls;
            nbObjs = 0;
            m_Text.text = $"{nbObjs} DClls";
        }
    }
}
