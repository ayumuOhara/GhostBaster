using UnityEngine;

public static class AlphaSetter
{
    public static Color FadeIn(Color color, float fadeSpd)
    {
        color.a += fadeSpd * Time.deltaTime;
        return color;
    }

    public static Color FadeOut(Color color, float fadeSpd, float minAlpha = 0)
    {
        color.a -= fadeSpd * Time.deltaTime;

        if (color.a < minAlpha)
            color.a = minAlpha;

        return color;
    }
}
