using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueInsertion : MonoBehaviour
{
    



    public int LevenshteinDistance(string a, string b)
    {
        int[][] dp = new int[30][];

        int l1 = a.Length;
        int l2 = b.Length;

        a = a.ToUpper();
        b = b.ToUpper();

        Debug.Log(a);
        Debug.Log(b);

        for (int i = 0; i <= l1; i++)
        {
            dp[i] = new int[30];
            dp[i][0] = i;

        }
        for (int i = 0; i <= l2; i++)
        {
            dp[0][i] = i;
        }

        for (int i = 1; i <= l1; i++)
        {
            for (int j = 1; j <= l2; j++)
            {
                dp[i][j] = Mathf.Min(dp[i - 1][j], dp[i][j - 1], dp[i - 1][j - 1]);

                if (a[i - 1] != b[j - 1])
                    dp[i][j]++;
            }
        }

        return dp[l1][l2];
    }
}
