using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding 
{
    [SerializeField]
    public int quantity;
    public float lastTicked;
    public int fishToCollect;
    Building building;

    public PlayerBuilding(Building building) {
        this.building = building;
        this.quantity = 0;
        this.fishToCollect = 0;
    }

    public void addQuantity(int quantity) {
        this.quantity += quantity;
    }

    public int getQuantity() {
        return this.quantity;
    }

    public int getFishPerTick() {
        int fishPerTick = this.building.baseFishPerTick * this.quantity;
        return fishPerTick;
    }

    public float getTickDelay() {
        return this.building.tickDelay;
    }

    public float getLastTicked() {
        return this.lastTicked;
    }

    public void setLastTicked(float tick) {
        this.lastTicked = tick;
    }

    public void addFishToCollect(int amount) {
        this.fishToCollect += amount;
    }

    public void clearFishToCollect() {
        this.fishToCollect = 0;
    }

    public int getFishToCollect() {
        return this.fishToCollect;
    }
}
