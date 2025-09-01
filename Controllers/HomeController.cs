using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FootballLeague.Models;
using FootballLeague.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var teams = await _context.Teams
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalsFor - t.GoalsAgainst)
                .ThenByDescending(t => t.GoalsFor)
                .ThenBy(t => t.GamesPlayed)
                .ToListAsync();

            if (teams != null && teams.Any())
            {
                // تحديث مراكز الفرق بناءً على النقاط وفارق الأهداف والأهداف المسجلة
                int currentPosition = 1;
                int samePositionCount = 0;

                for (int i = 0; i < teams.Count; i++)
                {
                    var team = teams[i];
                    var currentGoalDifference = team.GoalsFor - team.GoalsAgainst;

                    if (i > 0)
                    {
                        var previousTeam = teams[i - 1];
                        var previousGoalDifference = previousTeam.GoalsFor - previousTeam.GoalsAgainst;

                        if (team.Points == previousTeam.Points && 
                            currentGoalDifference == previousGoalDifference &&
                            team.GoalsFor == previousTeam.GoalsFor)
                        {
                            team.Position = previousTeam.Position;
                            samePositionCount++;
                        }
                        else
                        {
                            currentPosition = i + 1;
                            team.Position = currentPosition;
                            samePositionCount = 0;
                        }
                    }
                    else
                    {
                        team.Position = currentPosition;
                    }
                }

                ViewBag.Teams = teams;
                ViewBag.TeamsCount = teams.Count;
                ViewBag.MatchesCount = await _context.Matches.CountAsync();
                ViewBag.GoalsCount = await _context.Matches.SumAsync(m => m.HomeTeamScore + m.AwayTeamScore);
            }
            else
            {
                _logger.LogWarning("No teams found in the database");
                ViewBag.Teams = new List<Team>();
                ViewBag.TeamsCount = 0;
                ViewBag.MatchesCount = 0;
                ViewBag.GoalsCount = 0;
            }

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in Index action: {ex.Message}");
            return RedirectToAction(nameof(Error));
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
