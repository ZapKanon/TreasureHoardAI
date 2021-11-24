using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetTreasure : Action
{
    private Treasure targetTreasure;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("hasTreasure", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_DepositTreasure);
    }

    public override bool ValidAction()
    {
        if (perception.treasureInView.Count > 0 && character.carriedTreasure == null)
        {
            return true;
        }

        return false;
    }

    public override void ActionBegin()
    {
        targetTreasure = perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>();
        character.SetDestination(targetTreasure.transform.position);
    }

    public override void ActionEnd()
    {
        targetTreasure = null;
    }

    public override void UpdateAction()
    {
        targetTreasure = perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>();
        character.SetDestination(targetTreasure.transform.position);

        character.CheckDestination();
        if (character.reachedDestination)
        {
            if (character.carriedTreasure == null)
            {
                character.PickUpTreasure(perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>());
            }
        }
    }
}
