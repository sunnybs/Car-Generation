using System;
using Assets.Utils.Blueprints;

namespace Assets.Utils
{
    public class BlueprintManager
    {
        public static BaseBlueprint PickByCount(int transmissionCount)
        {
            if (transmissionCount <= 5) return new SimpleBlueprint();
            if (transmissionCount == 6) return new Rect3X2Blueprint();
            throw new ArgumentException();
        }
    }
}