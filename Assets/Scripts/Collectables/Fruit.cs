public enum FruitType
{
    Apple = 0,
    Grapes,
    Cherry,
}

public class Fruit : Collectable
{

    public FruitType type;

    protected override void OnRabbitHit(HeroRabbit rabit)
    {
        LevelController.current.addFruits(1, type);
        this.CollectedHide();
    }
}