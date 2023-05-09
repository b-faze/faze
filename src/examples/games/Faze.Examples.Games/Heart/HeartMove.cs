namespace Faze.Examples.Games.Heart
{
    public abstract class HeartMove
    {
        public abstract HeartMoveType Type { get; }
        public abstract HeartMoveEffect GetEffect(HeartMoveType type);
    }

    public class AttackMove : HeartMove
    {
        public override HeartMoveType Type => HeartMoveType.Attack;

        public override HeartMoveEffect GetEffect(HeartMoveType type)
        {
            switch (type)
            {
                case HeartMoveType.Attack:
                case HeartMoveType.Feint:
                case HeartMoveType.Defend:
                    return HeartMoveEffect.Create(1, 0, 0);
            }

            return HeartMoveEffect.None();
        }
    }

    public class FeintMove : HeartMove
    {
        public override HeartMoveType Type => HeartMoveType.Feint;

        public override HeartMoveEffect GetEffect(HeartMoveType type)
        {
            switch (type)
            {
                case HeartMoveType.Feint:
                case HeartMoveType.Defend:
                    return HeartMoveEffect.Create(0, 0, 1);
            }

            return HeartMoveEffect.None();
        }
    }

    public class DefendMove : HeartMove
    {
        public override HeartMoveType Type => HeartMoveType.Defend;

        public override HeartMoveEffect GetEffect(HeartMoveType type)
        {
            switch (type)
            {
                case HeartMoveType.Attack:
                    return HeartMoveEffect.Create(0, 1, 0);
                case HeartMoveType.Dodge:
                    return HeartMoveEffect.Create(0, 0, 1);
            }

            return HeartMoveEffect.None();
        }
    }

    public class DodgeMove : HeartMove
    {
        public override HeartMoveType Type => HeartMoveType.Dodge;

        public override HeartMoveEffect GetEffect(HeartMoveType type)
        {
            switch (type)
            {
                case HeartMoveType.Attack:
                    return HeartMoveEffect.Create(0, 0, 1);
            }

            return HeartMoveEffect.None();
        }
    }
}
