using UnityEngine;
using System.Collections;

public class Follower : NPC
{
    #region logic variables
    //follower stats
    int obedience = 0;
    int faith = 0;
    int resentment = 0;
    int goodBad = 0;
    int violence = 0;
    int suicide = 0;
   
    //follower limit checks
    bool obedienceCheck = false;
    bool faithCheck = false;
    bool resentmentCheck = false;
    bool goodCheck = false;
    bool badCheck = false;
    bool violenceCheck = false;
    bool suicideCheck = false;
    public bool succumbed = false;
    
    /// <summary>
    ///unchanging stats
    /// </summary>
    ///followers worth
    protected int money;
    //followers influence on others
    protected int influence;
    //followers suseptability to player
    protected int gulliblility;

    //follower limit
    private int maxStat = 100;
    private int maxO;
    private int maxF;
    private int maxR;
    private int maxGB;
    private int maxV;
    private int maxS;

    #endregion
    #region game variables
    public FollowerState fState;
    #endregion
    #region logic functions
    //sets stats in awake
    public void SetStartStats(int _money, int _influence, int _gullibility)
    {
        money = _money;
        influence = _influence;
        gulliblility = _gullibility;
    }

    //set stat limits in awake
    public void SetStatLimits(int _maxO, int _maxF,int _maxR, int _maxGB, int _maxV, int _maxS)
    {
        maxO = _maxO;
        maxF = _maxF;
        maxR = _maxR;
        maxGB = _maxGB;
        maxV = _maxV;
        maxS = _maxS;
    }

    //check stats in checkallStats
    private bool StatCheck(int stat)
    {
        if (stat >= maxStat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //check stats in update
    public void CheckAllStats()
    {
        obedienceCheck = StatCheck(obedience);
        faithCheck = StatCheck(faith);
        resentmentCheck = StatCheck(resentment);
        goodCheck = StatCheck(goodBad);
        badCheck = StatCheck(goodBad * -1);
        violenceCheck = StatCheck(violence);
        suicideCheck = StatCheck(suicide);
    }
  
    //gets selected stat
   public int StatGetter(StatSelect statType)
   {
       switch (statType)
       {
           case StatSelect.FAITH:
               {
                   return faith;
               }
           case StatSelect.GOODBAD:
               {
                   return goodBad;
               }    
           case StatSelect.OBEDIENCE:
               {
                   return obedience;
               }
           case StatSelect.RESENTMENT:
               {
                   return resentment;
               }
           case StatSelect.SUICIDE:
               {
                   return suicide;
               }
           case StatSelect.VIOLENCE:
               {
                   return violence;
               }
       }
       return 0;
   }

   public void StatSetter(StatSelect statType, int amount)
   {
       switch (statType)
       {
           case StatSelect.FAITH:
               {
                   faith += amount;
                   break;
               }
           case StatSelect.GOODBAD:
               {
                   goodBad += amount;
                   break;
               }
           case StatSelect.OBEDIENCE:
               {
                   obedience += amount;
                       break;
               }
           case StatSelect.RESENTMENT:
               {
                   resentment += amount;
                   break;
               }
           case StatSelect.SUICIDE:
               {
                   suicide += amount;
                   break;
               }
           case StatSelect.VIOLENCE:
               {
                   violence += amount;
                   break;
               }
        }
   }
#endregion
    #region game functions
   

    #endregion
    public override void Awake()
    {
        base.Awake();
        SetHealth(50);
    }
   public override void Update()
    {
      //  base.Update();
        Death();
        FadeAtDeath();
    }

}

public enum StatSelect
{
 OBEDIENCE,
 FAITH,
 RESENTMENT,
 GOODBAD,
 VIOLENCE,
 SUICIDE
}
public enum FollowerState
{
   

}