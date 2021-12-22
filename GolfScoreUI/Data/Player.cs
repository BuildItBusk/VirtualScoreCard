namespace GolfScoreUI.Data
{
    public class Player
    {   
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
