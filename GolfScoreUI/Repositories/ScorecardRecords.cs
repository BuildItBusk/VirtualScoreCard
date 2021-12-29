namespace GolfScoreUI.Repositories;

public record struct PlayerDto(Guid Id, string Name);
public record struct ScoreDto(Guid PlayerId, int HoleNumber, int Strokes);
public record struct ScorecardDto(Guid Id, List<PlayerDto> Players, List<ScoreDto> Scores, int MaxStrokes, int NumberOfHoles);
