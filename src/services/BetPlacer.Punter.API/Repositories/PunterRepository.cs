using BetPlacer.Punter.API.Config;
using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

public class PunterRepository
{
    private readonly PunterDbContext _context;

    public PunterRepository(DbContextOptions<PunterDbContext> db)
    {
        _context = new PunterDbContext(db);
    }

    public async Task<List<MatchBaseData>> GetMatchBaseDataAsync(int leagueCode)
    {
        var query = Queries.GetMatchBaseDataQuery(leagueCode);

        List<MatchBaseData> baseData = await _context.MatchBaseData
            .FromSqlInterpolated(FormattableStringFactory.Create(query))
            .ToListAsync();

        return baseData;
    }
}