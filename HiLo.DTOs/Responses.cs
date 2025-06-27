using HiLo.Core;

namespace HiLo.DTOs;

public record JoinGameResponse(bool success);
public record ProgressGameResponse(GameState? gameState);
public record GameDetailsResponse(string gameKey, string[] players, int numberOfRounds);
public record StartGameResponse(string gameKey, int numberOfRounds);
public record StartRoundResponse(int currentRound, Player[] players);
public record UpdateRoundsResponse(int numberOfRounds);
public record GameWonResponse(Player winningPlayer);

