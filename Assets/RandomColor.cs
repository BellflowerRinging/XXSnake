using UnityEngine;

public class RandomColor 
{

    float[] ColorPercent = { 20, 20, 20, 20, 20 };

    public Color GetRandomColor()
    {
        int i = (int)Choose(ColorPercent);
        switch (i)
        {
            case 0: return Color.magenta;
            case 1: return Color.blue;
            case 2: return Color.red;
            case 3: return Color.yellow;
            case 4: return Color.green;
            default: return Color.black;
        }
    }
    public void SetColorPercent(Color ColorIndex, float AddPercent)
    {
        int i = 0;
       
        if (ColorIndex == Color.magenta)
        {
            i =0;
        }
        else if (ColorIndex == Color.blue)
        {
            i = 1;
        }
        else if (ColorIndex == Color.red)
        {
            i = 2;
        }
        else if (ColorIndex == Color.yellow)
        {
            i = 3;
        }
        else if (ColorIndex == Color.green)
        {
            i = 4;
        }

        SetColorPercent(i, AddPercent);
    }


    public void SetColorPercent(int ColorIndex, float AddPercent)
    {
        float x = 100 - ColorPercent[ColorIndex];

        ColorPercent[ColorIndex] += AddPercent;

        for (int i = 0; i < ColorPercent.Length; i++)
        {
            if (i == ColorIndex) continue;

            if (ColorPercent[i] == 0)
            {
                ColorPercent[i] -= AddPercent * (1f / (ColorPercent.Length - 1));
            }
            else
            {
                ColorPercent[i] -= AddPercent * (ColorPercent[i] / x);
            }
        }

        if (ColorPercent[ColorIndex] < 0)
        {
            SetColorPercent(ColorIndex, -ColorPercent[ColorIndex]);
        }
        else if (ColorPercent[ColorIndex] > 100)
        {
            SetColorPercent(ColorIndex, 100 - ColorPercent[ColorIndex]);
        }
    }

    float Choose(float[] Probs)
    {
        float total = 0;
        foreach (float elem in Probs)
            total += elem;

        float randomPoint = Random.value * total;

        for (int i = 0; i < Probs.Length; i++)
        {
            if (randomPoint < Probs[i]) return i;
            else randomPoint -= Probs[i];
        }
        return Probs.Length - 1;
    }


}