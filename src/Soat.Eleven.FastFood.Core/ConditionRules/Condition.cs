﻿namespace Soat.Eleven.FastFood.Core.ConditionRules;

public class Condition
{
    public static ConditionException<T> Require<T>(T target)
    {
        return new RequiredException<T>(target, "value");
    }

    public static ConditionException<T> Require<T>(T target, string argument)
    {
        return new RequiredException<T>(target, argument);
    }
}
