using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OkapiKit;

public class DoNothing : Action
{
    public override void Execute()
    {
        if (!enableAction) return;
        if (!EvaluatePreconditions()) return;
    }

    public override string GetRawDescription(string ident, GameObject refObject)
    {
        string desc = GetPreconditionsString(gameObject);

        desc += $"do nothing";

        return desc;
    }
}
