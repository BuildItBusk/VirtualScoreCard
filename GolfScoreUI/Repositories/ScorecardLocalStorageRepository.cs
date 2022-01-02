using GolfScoreUI.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GolfScoreUI.Repositories
{
    public class ScorecardLocalStorageRepository : IScorecardRepository
    {
        private readonly ProtectedLocalStorage _storage;

        public ScorecardLocalStorageRepository(ProtectedLocalStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
    
        public async Task Create(Scorecard scorecard)
        {
            var dto = new ScorecardDto
            {
                Id = scorecard.Id,
                MaxStrokes = scorecard.MaxStrokes,
                NumberOfHoles = scorecard.NumberOfHoles
            };

            dto.Players = new();
            foreach (var player in scorecard.Players)
                dto.Players.Add(new PlayerDto(player.Id, player.Name));

            dto.Scores = new();
            foreach (var score in scorecard.Scores)
                dto.Scores.Add(new ScoreDto(score.PlayerId, score.HoleNumber, score.NumberOfStrokes));

            await _storage.SetAsync("scorecard", dto);
        }

        public async Task Update(Scorecard scorecard)
        {
            // In local storage, creating and updating is the same. 
            await Create(scorecard);
        }

        public async Task<Scorecard> GetLatest()
        {
            var result = await _storage.GetAsync<ScorecardDto>("scorecard");
            var dto = result.Value;

            if (dto.Players is null || dto.Scores is null)
                throw new InvalidOperationException("Failed to load scorecard. Players and/or scores are missing.");

            var players = new List<Player>();
            foreach (var player in dto.Players)
                players.Add(new Player { Id = player.Id, Name=player.Name });

            var scores = new List<HoleScore>();
            foreach (var score in dto.Scores)
                scores.Add(new HoleScore(score.PlayerId, score.HoleNumber, dto.MaxStrokes) { NumberOfStrokes = score.Strokes });

            return new Scorecard(dto.Id, dto.NumberOfHoles, dto.MaxStrokes, players, scores);
        }
    }
}