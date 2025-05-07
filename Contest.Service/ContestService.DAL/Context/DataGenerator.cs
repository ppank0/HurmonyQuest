using Bogus;
using ContestService.DAL.Entities;

namespace ContestService.DAL.Context;
public class DataGenerator
{
    public static DateTime timeAt = DateTime.SpecifyKind(new DateTime(2025, 5, 1, 12, 0, 0), DateTimeKind.Utc);
    public static Faker<Jury> GetJuryGenerator()
    {
        return new Faker<Jury>()
            .RuleFor(j => j.Id, f => Guid.NewGuid())
            .RuleFor(j => j.Name, f => f.Name.FirstName())
            .RuleFor(j => j.Surname, f => f.Name.LastName())
            .RuleFor(j => j.Birthday, f => DateOnly.FromDateTime(f.Date.Past(60, DateTime.UtcNow.AddYears(-18))))
            .RuleFor(j => j.UserId, f => Guid.NewGuid())
            .RuleFor(j => j.CreatedAt, _ => timeAt)
            .RuleFor(j => j.UpdatedAt, _ => timeAt);
    }

    public static Faker<Participant> GetParticipantGenerator(Guid musicalInstrumentId)
    {
        return new Faker<Participant>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.FirstName())
            .RuleFor(p => p.Surname, f => f.Name.LastName())
            .RuleFor(p => p.Birthday, f => DateOnly.FromDateTime(f.Date.Past(60, DateTime.UtcNow.AddYears(-18))))
            .RuleFor(p => p.MusicalInstrumentId, _ => musicalInstrumentId)
            .RuleFor(p => p.UserId, f => Guid.NewGuid())
            .RuleFor(j => j.CreatedAt, _ => timeAt)
            .RuleFor(j => j.UpdatedAt, _ => timeAt);
    }

    public static Faker<Nomination> GetNominationGenerator()
    {
        return new Faker<Nomination>()
            .RuleFor(n => n.Id, f => Guid.NewGuid())
            .RuleFor(n => n.Name, f => f.Music.Genre())
            .RuleFor(j => j.CreatedAt, _ => timeAt)
            .RuleFor(j => j.UpdatedAt, _ => timeAt);
    }

    public static Faker<Stage> GetStageGenerator()
    {
        return new Faker<Stage>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Name, f => f.Lorem.Word())
            .RuleFor(s => s.StartDate, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
            .RuleFor(s => s.EndDate, (f, s) => s.StartDate.AddDays(f.Random.Int(1, 2)))
            .RuleFor(j => j.CreatedAt, _ => timeAt)
            .RuleFor(j => j.UpdatedAt, _ => timeAt);
    }

    public static Faker<MusicalInstrument> GetMusicalInstrumentGenerator(Guid nominationId)
    {
        return new Faker<MusicalInstrument>()
            .RuleFor(i => i.Id, f => Guid.NewGuid())
            .RuleFor(i => i.Name, f => f.Commerce.ProductName())
            .RuleFor(i => i.NominationId, _ => nominationId)
            .RuleFor(j => j.CreatedAt, _ => timeAt)
            .RuleFor(j => j.UpdatedAt, _ => timeAt);
    }

    public static readonly List<Jury> Juries = new();
    public static readonly List<Participant> Participants = new();
    public static readonly List<Nomination> Nominations = new();
    public static readonly List<Stage> Stages = new();
    public static readonly List<MusicalInstrument> MusicalInstruments = new();

    public const int NumberOfJuries = 5;
    public const int NumberOfParticipants = 4;
    public const int NumberOfNominations = 3;
    public const int NumberOfStages = 3;
    public const int NumberOfMusicalInstrumentsPerNomination = 4;

    public static void InitBogusJuryData()
    {
        var juryGenerator = GetJuryGenerator();
        var generatedJuries = juryGenerator.Generate(NumberOfJuries);
        Juries.AddRange(generatedJuries);
    }
    public static void InitBogusStageData()
    {
        var stageGenerator = GetStageGenerator();
        var generatedStages = stageGenerator.Generate(NumberOfStages);
        Stages.AddRange(generatedStages);
    }
    public static List<Nomination> GetBogusNominationData()
    {
        var nominationGenerator = GetNominationGenerator();
        var generatedNominations = nominationGenerator.Generate(NumberOfNominations);
        Nominations.AddRange(generatedNominations);

        return generatedNominations;
    }

    public static List<MusicalInstrument> GetBogusMusicalInstrumentData(Guid nominationId)
    {
        var musicalInstrumentGenerator = GetMusicalInstrumentGenerator(nominationId);
        var generatedMusicalInstruments = musicalInstrumentGenerator.Generate(NumberOfMusicalInstrumentsPerNomination);
        MusicalInstruments.AddRange(generatedMusicalInstruments);

        return generatedMusicalInstruments;
    }

    public static List<Participant> GetBogusParticipantData(Guid musicalInstrumentId)
    {
        var participantGenerator = GetParticipantGenerator(musicalInstrumentId);
        var generatedParticipants = participantGenerator.Generate(NumberOfParticipants);
        Participants.AddRange(generatedParticipants);

        return generatedParticipants;
    }

    public static void InitBogusData()
    {
        if (!Juries.Any() &&
            !Stages.Any() &&
            !Nominations.Any() &&
            !MusicalInstruments.Any() &&
            !Participants.Any())
        {
            InitBogusJuryData();
            InitBogusStageData();
            var nominations = GetBogusNominationData();

            foreach (var nomination in nominations)
            {
                var instruments = GetBogusMusicalInstrumentData(nomination.Id);

                foreach (var instrument in instruments)
                {
                    GetBogusParticipantData(instrument.Id);
                }
            }
        }
    }
}
