using GolfScoreUI.Data;

namespace GolfScoreUI.Repositories
{
    public interface IScorecardRepository
    {
        Task Create(Scorecard scorecard);
        Task Update(Scorecard scorecard);
        Task<Scorecard> GetLatest();
    }
}