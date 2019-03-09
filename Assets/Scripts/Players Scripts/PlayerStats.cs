using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {
    public enum PlayerState { idle, walk };

    private int maxHealthPoints;
    private int currentHealthPoints;
    private int maxStamina;
    private int currentStamina;
    private int NumberOfGold;

    private bool[] ItemsAquired;

    private int[] LanthernSkillsCost;
    private bool[] LanthernBought;

    private int[] SwordSkillsCost;
    private bool[] SwordBought;

    private int[] HammerSkillsCost;
    private bool[] HammerBougth;

    private int[] BowSkillsCost;
    private bool[] BowBought;

    private PlayerState currentState;

    //=============================================================================== constructor ==========================================

    public PlayerStats(int hp = 100, int sp = 100, int itmnum = 8, int lspl = 5, int sspl= 5, int hspl =5, int bspl = 5) {
        maxHealthPoints = hp;
        currentHealthPoints = hp;
        maxStamina = sp;
        currentStamina = sp;
        NumberOfGold = 0;

        ItemsAquired = new bool[itmnum];
        LanthernSkillsCost = new int[lspl];
        LanthernBought = new bool[lspl];
        SwordSkillsCost = new int[sspl];
        SwordBought = new bool[sspl];
        HammerSkillsCost = new int[hspl];
        HammerBougth = new bool[hspl];
        BowSkillsCost = new int[bspl];
        BowBought = new bool[bspl];

    }

    //=============================================================================== setters ==============================================
    
    public void setMaxHP(int hp) {
        maxHealthPoints = hp;
    }

    public void setCurrentHP(int hp) {
        currentHealthPoints = hp;
    }

    public void setMaxStamina(int stamina) {
        maxStamina = stamina;
    }

    public void setCurrentStamina(int stamina) {
        currentStamina = stamina;
    }

    public void setGold(int gold) {
        NumberOfGold = gold;
    }

    public void setState(PlayerState state) {
        currentState = state;
    }

    public void setItemPos(int pos, bool val) {
        ItemsAquired[pos] = val;
    }

    public void setLanCostPos(int pos, int val) {
        LanthernSkillsCost[pos] = val;
    }

    public void setLanBoughtPos(int pos, bool val) {
        LanthernBought[pos] = val;
    }

    public void setSwordCostPos(int pos, int val) {
        SwordSkillsCost[pos] = val;
    }

    public void setSwordBoughtPos(int pos, bool val) {
        SwordBought[pos] = val;
    }

    public void setHammCostPos(int pos, int val) {
        HammerSkillsCost[pos] = val;
    }

    public void setHammBoughtPos(int pos, bool val) {
        HammerBougth[pos] = val;
    }

    public void setBowCostPos(int pos, int val) {
        BowSkillsCost[pos] = val;
    }

    public void setBowBoughtPos(int pos, bool val) {
        BowBought[pos] = val;
    }

    // ============================================================================= getters =================================================

    public int getMaxHP() {
        return maxHealthPoints;
    }

    public int getCurrentHP() {
        return currentHealthPoints;
    }

    public int getMaxStamina() {
        return maxStamina;
    }

    public int getCurrentStamina() {
        return currentStamina;
    }

    public int getGold() {
        return NumberOfGold;
    }

    public PlayerState getState() {
        return currentState;
    }

    public bool getItemPos(int pos) {
        return ItemsAquired[pos];
    }

    public int getLanCostPos(int pos) {
        return LanthernSkillsCost[pos];
    }

    public bool getLanBoughtPos(int pos) {
        return LanthernBought[pos];
    }

    public int getSwordCostPos(int pos) {
        return SwordSkillsCost[pos];
    }

    public bool getSwordBoughtPos(int pos) {
        return SwordBought[pos];
    }

    public int getHammCostPos(int pos) {
        return HammerSkillsCost[pos];
    }

    public bool getHammBoughtPos(int pos) {
        return HammerBougth[pos];
    }

    public int getBowCostPos(int pos) {
        return BowSkillsCost[pos];
    }

    public bool getBowBoughtPos(int pos) {
        return BowBought[pos];
    }

}
