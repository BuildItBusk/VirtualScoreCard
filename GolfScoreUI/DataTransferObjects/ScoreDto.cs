namespace GolfScoreUI.DataTransferObjects;

public record struct ScoreDto(Guid PlayerId, int HoleNumber, int Strokes);
