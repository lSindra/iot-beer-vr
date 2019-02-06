using UnityEngine;
using Valve.VR.InteractionSystem;

public class MovementGuide : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float m_ShowAngle = 10f;            // How far from the desired facing direction the player must be facing for the arrows to appear.
    [SerializeField] private float m_MaxAlphaAngle = 60f;       // How far from the desired facing direction the player can be facing for the arrows to appear.
    [SerializeField] private Transform m_DesiredDirection;      // Indicates which direction the player should be facing (uses world space forward if null).
    [SerializeField] private Renderer m_ArrowUp;                // Reference to the up renderer of the arrow used to fade in and out.
    [SerializeField] private Renderer m_ArrowDown;              // Reference to the down renderer of the arrow used to fade in and out.
    [SerializeField] private Renderer m_ArrowLeft;              // Reference to the left renderer of the arrow used to fade in and out.
    [SerializeField] private Renderer m_ArrowRight;             // Reference to the right renderer of the arrow used to fade in and out.

    private Transform m_Camera;

    private const string k_MaterialPropertyName = "_Alpha";     // The name of the alpha property on the shader being used to fade the arrows.


    private void Start ()
    {
        m_Camera = player.hmdTransform;
    }


    private void Update()
    {
        Vector3 desiredForward = m_DesiredDirection == null ? Vector3.forward : m_DesiredDirection.forward;

        UpdateSides(desiredForward);
        UpdateVertical();
    }

    private void UpdateVertical()
    {
        Vector3 flatCamRight = Vector3.ProjectOnPlane(m_Camera.forward, Vector3.right);

        float angleDeltaRight = flatCamRight.y * 90;

        float m_CurrentAlphaUp = (angleDeltaRight - m_ShowAngle) / (m_MaxAlphaAngle - m_ShowAngle);
        if (angleDeltaRight > m_MaxAlphaAngle || angleDeltaRight < -m_MaxAlphaAngle) m_CurrentAlphaUp = 0;

        UpdateArrow(m_ArrowUp, m_CurrentAlphaUp);
        UpdateArrow(m_ArrowDown, -m_CurrentAlphaUp);
    }

    private void UpdateSides(Vector3 desiredForward)
    {
        Vector3 flatCamForward = Vector3.ProjectOnPlane(m_Camera.forward, Vector3.up);

        float angleDeltaForward = Vector3.SignedAngle(desiredForward, flatCamForward, Vector3.up);

        float m_CurrentAlphaSide = (angleDeltaForward - m_ShowAngle) / (m_MaxAlphaAngle - m_ShowAngle);
        if (angleDeltaForward > m_MaxAlphaAngle || angleDeltaForward < -m_MaxAlphaAngle) m_CurrentAlphaSide = 0;

        UpdateArrow(m_ArrowRight, m_CurrentAlphaSide);
        UpdateArrow(m_ArrowLeft, -m_CurrentAlphaSide);
    }

    private void UpdateArrow(Renderer arrow, float alpha)
    {
        if (arrow != null)
        {
            arrow.material.SetFloat(k_MaterialPropertyName, alpha);
        }
    }
}