namespace GolfScoreUI.Data
{
    public class Player
    {   
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
