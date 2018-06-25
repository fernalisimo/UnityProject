public class Gem : Collectable
{
    public CrystalColor color;

    protected override void OnRabbitHit(HeroRabbit rabit)
    {
        CrystalPanel.current.addCrystal(color);
        this.CollectedHide();
    }
}