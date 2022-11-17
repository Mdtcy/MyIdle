﻿public class OnCastStartedAbilityEvent : AbilityEventBase
{
    public AbilityBase Parentability;

    public OnCastStartedAbilityEvent(AbilityBase parentAbility, AbilityActionBase bindedAction)
        : base(bindedAction)
    {
        Parentability = parentAbility;

        Bind();
    }

    protected override void Bind()
    {
        Parentability.OnCastStarted += OnCastStarted;
    }

    protected override void Unbind()
    {
        Parentability.OnCastStarted += OnCastStarted;
        TriggerActionList.Clear();
    }

    private void OnCastStarted()
    {
        Trigger();

        Unbind();
    }

}
