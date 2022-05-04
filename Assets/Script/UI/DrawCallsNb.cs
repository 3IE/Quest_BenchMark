using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class DrawCallsNb : MonoBehaviour
    { // Not in use yet, as I haven't found a way to get drawCall nb once the app is build
        private int nbObjs;
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            InvokeRepeating(nameof(DrawNb), 1, 1);
        }

        private void DrawNb()
        {
            // possibilities: smoothDeltaTime, deltaTime, fixedDeltaTime
            nbObjs = 0;
            m_Text.text = $"{nbObjs} DClls";
        }
    }
}
