using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IConferenceService
    {
        Task AddConferenceAsync(Conference conference);
        Task DeleteConferenceAsync(int conferenceId);
        Task<Conference> GetConferencesByIdAsync(int conferenceId);
        Task<IEnumerable<Conference>> GetAllConferencesAsync();
        Task UpdateConferenceAsync(Conference conference);
    }
}
