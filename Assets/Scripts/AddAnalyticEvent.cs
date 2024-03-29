using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OkapiKit;
using Unity.Services.Analytics;
using Event = Unity.Services.Analytics.Event;
using Unity.Services.Core;

public class AddAnalyticEvent : Action
{
    [SerializeField] private string eventName = "Unnamed";
    [SerializeField] private string extraData = "";

    static bool initialized = false;

    public class OkapiEvent : Event
    {       
        public OkapiEvent(string eventName, string extraData) : base(eventName)
        {
            SetParameter("time", Time.timeSinceLevelLoad);
            SetParameter("extraData", extraData);
        }
    }

    async private void Start()
    {
        if (!initialized)
        {
            initialized = true;

            Debug.Log("Initializing UnityServices");
            await UnityServices.InitializeAsync();

            Debug.Log("Starting collection");
            AnalyticsService.Instance.StartDataCollection();
        }
    }

    public override void Execute()
    {
        if (!enableAction) return;
        if (!EvaluatePreconditions()) return;
        if (initialized)
        {
            AnalyticsService.Instance.RecordEvent(new OkapiEvent(eventName, extraData));
        }
    }

    public override string GetRawDescription(string ident, GameObject refObject)
    {
        string desc = GetPreconditionsString(gameObject);

        desc += $"add an event on the analytics called [{eventName}]";

        return desc;
    }
}
