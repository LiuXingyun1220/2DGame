using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public FragmentPanel fragmentPanel;
    public int fragmentIndexToUnlock; // 当前关卡解锁的 Fragment 索引

    void LevelFinished()
    {
        fragmentPanel.UnlockFragment(fragmentIndexToUnlock);
    }
}

