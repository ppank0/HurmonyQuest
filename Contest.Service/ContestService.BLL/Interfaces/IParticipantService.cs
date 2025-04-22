using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface IParticipantService
{
    Task<List<ParticipantModel>> GetAllAsync(CancellationToken ct);
    Task<ParticipantModel> GetAsync(Guid id, CancellationToken ct);
    Task<ParticipantModel> CreateAsync(ParticipantModel model, CancellationToken ct);
    Task<ParticipantModel> UpdateAsync(ParticipantModel model, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
