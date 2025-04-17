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
    Task<ParticipantModel> GetParticipantByIdAsync(Guid id, CancellationToken ct);
    Task<ParticipantModel> CreateParticipantAsync(ParticipantModel model, CancellationToken ct);
    Task<ParticipantModel> UpdateParticipantAsync(ParticipantModel model, CancellationToken ct);
    Task DeleteParticipantAsync(Guid id, CancellationToken ct);
}
