using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Kiritishni (klaviatura, sichqoncha, sensor) bitta joyda yig'adigan yordamchi sinf.
/// Yangi Input System ham, eski Input Manager ham ishlaydi.
/// </summary>
public static class Inp
{
#if ENABLE_INPUT_SYSTEM
    static Keyboard K { get { return Keyboard.current; } }

    public static bool TapDown()
    {
        bool key = K != null && K.spaceKey.wasPressedThisFrame;
        Pointer p = Pointer.current;
        return key || (p != null && p.press.wasPressedThisFrame);
    }

    public static bool PointerHeld()
    {
        Pointer p = Pointer.current;
        return p != null && p.press.isPressed;
    }

    public static Vector2 PointerPos()
    {
        Pointer p = Pointer.current;
        return p != null ? p.position.ReadValue() : Vector2.zero;
    }

    public static bool SpaceHeld() { return K != null && K.spaceKey.isPressed; }
    public static bool RDown() { return K != null && K.rKey.wasPressedThisFrame; }
    public static bool CDown() { return K != null && K.cKey.wasPressedThisFrame; }

    public static Vector2 WASD()
    {
        if (K == null) return Vector2.zero;
        float x = (K.dKey.isPressed ? 1f : 0f) - (K.aKey.isPressed ? 1f : 0f);
        float y = (K.wKey.isPressed ? 1f : 0f) - (K.sKey.isPressed ? 1f : 0f);
        return new Vector2(x, y);
    }
#else
    public static bool TapDown() { return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0); }
    public static bool PointerHeld() { return Input.GetMouseButton(0); }
    public static Vector2 PointerPos() { return Input.mousePosition; }
    public static bool SpaceHeld() { return Input.GetKey(KeyCode.Space); }
    public static bool RDown() { return Input.GetKeyDown(KeyCode.R); }
    public static bool CDown() { return Input.GetKeyDown(KeyCode.C); }
    public static Vector2 WASD()
    {
        float x = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        float y = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
        return new Vector2(x, y);
    }
#endif
}
