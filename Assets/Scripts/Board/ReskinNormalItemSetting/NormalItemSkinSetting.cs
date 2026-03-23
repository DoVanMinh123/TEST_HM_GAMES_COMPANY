using UnityEngine;

public class NormalItemSkinSetting : ScriptableObject
{
    public Sprite TypeOne;
    public Sprite TypeTwo;
    public Sprite TypeThree;
    public Sprite TypeFour;
    public Sprite TypeFive;
    public Sprite TypeSix;
    public Sprite TypeSeven;

    public Sprite GetSprite(NormalItem.eNormalType type)
    {
        switch (type)
        {
            case NormalItem.eNormalType.TYPE_ONE: return TypeOne;
            case NormalItem.eNormalType.TYPE_TWO: return TypeTwo;
            case NormalItem.eNormalType.TYPE_THREE: return TypeThree;
            case NormalItem.eNormalType.TYPE_FOUR: return TypeFour;
            case NormalItem.eNormalType.TYPE_FIVE: return TypeFive;
            case NormalItem.eNormalType.TYPE_SIX: return TypeSix;
            case NormalItem.eNormalType.TYPE_SEVEN: return TypeSeven;
        }

        return null;
    }
}