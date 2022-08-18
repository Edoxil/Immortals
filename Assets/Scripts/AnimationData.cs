using Morpeh;

namespace Immortals
{
    [System.Serializable]
    public class AnimationData
    {
        public int EntityID;
        public bool IsControlledByUser;
        public ParameterType ParameterType;
        public string Key;

        public float FloatData;
        public int IntData;
        public bool BoolData;
        public bool ResetTrigger;
    }

    public enum ParameterType
    {
        None,
        Float,
        Bool,
        Int,
        Trigger
    }

}