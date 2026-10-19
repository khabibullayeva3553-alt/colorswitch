using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Color Switch o'yin menejeri: boshlash, ochko hisoblash, halok bo'lish va qayta boshlash.
/// Boshqaruv: Space yoki sichqoncha/ekranga bosish - sakrash.
/// </summary>
public class ColorSwitchGame : MonoBehaviour
{
    public BallController ball;
    public Camera cam;

    enum State { Waiting, Playing, Dead }
    State state = State.Waiting;
    int score;
    int best;
    float deathTime;
    GUIStyle big, mid;

    void Start()
    {
        best = PlayerPrefs.GetInt("ColorSwitchBest", 0);
        ball.SetColor(Random.Range(0, GameColors.Palette.Length));
    }

    void Update()
    {
        switch (state)
        {
            case State.Waiting:
                if (Inp.TapDown())
                {
                    ball.Begin();
                    ball.Jump();
                    state = State.Playing;
                }
                break;

            case State.Playing:
                Playing();
                break;

            case State.Dead:
                if (Inp.TapDown() && Time.unscaledTime - deathTime > 0.5f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
    }

    void Playing()
    {
        if (Inp.TapDown()) ball.Jump();

        Vector2 p = ball.transform.position;
        foreach (ObstacleRing r in ObstacleRing.All)
        {
            if (r.Hits(p, ball.radius, ball.colorIndex))
            {
                Die();
                return;
            }
            if (!r.scored && p.y > r.transform.position.y + r.outerRadius + 0.2f)
            {
                r.scored = true;
                score++;
            }
        }

        if (p.y < cam.transform.position.y - cam.orthographicSize - 0.5f) Die();
    }

    void Die()
    {
        state = State.Dead;
        deathTime = Time.unscaledTime;
        ball.Die();
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("ColorSwitchBest", best);
        }
    }

    static void Fill(Rect r, Color c)
    {
        Color old = GUI.color;
        GUI.color = c;
        GUI.DrawTexture(r, Texture2D.whiteTexture);
        GUI.color = old;
    }

    void OnGUI()
    {
        if (big == null)
        {
            big = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            mid = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        }

        float s = Screen.height / 720f;
        float w = Screen.width;
        float h = Screen.height;
        big.fontSize = Mathf.RoundToInt(64f * s);
        mid.fontSize = Mathf.RoundToInt(24f * s);
        big.normal.textColor = Color.white;
        mid.normal.textColor = Color.white;

        GUI.Label(new Rect(0, 16f * s, w, 90f * s), score.ToString(), big);

        if (state == State.Waiting)
        {
            GUI.Label(new Rect(0, h * 0.72f, w, 50f * s), "Boshlash uchun SPACE yoki bosing", mid);
        }
        else if (state == State.Dead)
        {
            Fill(new Rect(w * 0.5f - 240f * s, h * 0.5f - 110f * s, 480f * s, 220f * s), new Color(0f, 0f, 0f, 0.75f));
            GUI.Label(new Rect(0, h * 0.5f - 100f * s, w, 60f * s), "O'YIN TUGADI", mid);
            GUI.Label(new Rect(0, h * 0.5f - 40f * s, w, 50f * s), "Ochko: " + score + "     Rekord: " + best, mid);
            GUI.Label(new Rect(0, h * 0.5f + 25f * s, w, 50f * s), "Qayta boshlash: SPACE yoki bosing", mid);
        }
    }
}
