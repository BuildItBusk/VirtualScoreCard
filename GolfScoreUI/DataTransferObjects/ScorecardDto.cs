namespace GolfScoreUI.DataTransferObjects;
public record struct ScorecardDto(Guid Id, List<PlayerDto> Players, List<ScoreDto> Scores, int MaxStrokes, int NumberOfHoles);